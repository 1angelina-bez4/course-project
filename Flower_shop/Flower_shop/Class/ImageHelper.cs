using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Flower_shop
{
    public static class ImageHelper
    {
        // Поддерживаемые форматы изображений
        private static readonly string[] SupportedExtensions =
            { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp" };

        // Имя файла-заглушки
        private const string FallbackFileName = "free-icon-image-17807769.png";

        // Статическая переменная для хранения заглушки в памяти (загружается только 1 раз)
        private static Image _fallbackImage = null;

        public static void LoadImagesToTable(DataTable table, string fileNameColumn,
    string newImageColumn, string folderName = "Image")
        {
            if (table == null || !table.Columns.Contains(fileNameColumn)) return;

            table.Columns.Add(newImageColumn, typeof(Image));
            string folderPath = Path.Combine(Application.StartupPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show($"Папка '{folderName}' не найдена по пути:\n{folderPath}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем список всех файлов в папке (имя без расширения → полный путь)
            var existingFiles = Directory.GetFiles(folderPath)
                .Where(f => Path.GetFileName(f).ToLower() != FallbackFileName.ToLower())
                .ToDictionary(
                    f => Path.GetFileNameWithoutExtension(f).ToLower(),
                    f => f
                );

            int loadedCount = 0;
            int notFoundCount = 0;
            var notFoundNames = new List<string>(); 

            foreach (DataRow row in table.Rows)
            {
                string fileName = row[fileNameColumn]?.ToString()?.Trim();
                Image loadedImage = null;

                if (!string.IsNullOrEmpty(fileName))
                {
                    // Убираем расширение из имени, чтобы искать независимо от формата
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName).ToLower();


                    string fullPath = Path.Combine(folderPath, fileName);
                    if (File.Exists(fullPath))
                    {
                        loadedImage = TryLoadImage(fullPath);
                    }
                    else if (existingFiles.TryGetValue(nameWithoutExt, out string foundPath))
                    {
                        loadedImage = TryLoadImage(foundPath);
                    }
                    else
                    {
                        notFoundNames.Add($"«{fileName}» (в БД) → не найдено в папке");
                    }
                }

                if (loadedImage != null)
                {
                    row[newImageColumn] = loadedImage;
                    loadedCount++;
                }
                else
                {
                    row[newImageColumn] = GetFallbackImage(folderPath);
                    notFoundCount++;
                }
            }

            if (notFoundCount > 0)
            {
                string report = $"Загружено: {loadedCount}\nПрименена заглушка: {notFoundCount}\n\n" +
                                "Не найдены файлы:\n" +
                                string.Join("\n", notFoundNames.Take(10)); // Показываем первые 10

                if (notFoundNames.Count > 10)
                    report += $"\n... и ещё {notFoundNames.Count - 10}";

                MessageBox.Show(report, "Информация о загрузке изображений",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            table.Columns[fileNameColumn].ColumnMapping = MappingType.Hidden;
        }

        /// <summary>
        ///  Получает изображение-заглушку (загружает с диска только при первом вызове)
        /// </summary>
        private static Image GetFallbackImage(string folderPath)
        {
            // Если уже загрузили ранее, просто возвращаем из памяти (работает мгновенно)
            if (_fallbackImage != null)
                return _fallbackImage;

            string fallbackPath = Path.Combine(folderPath, FallbackFileName);

            if (File.Exists(fallbackPath))
            {
                try
                {
                    using (var stream = new FileStream(fallbackPath, FileMode.Open, FileAccess.Read))
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        ms.Position = 0;
                        _fallbackImage = Image.FromStream(ms);
                    }
                }
                catch
                {
                    // Если заглушка битая, возвращаем null
                }
            }

            return _fallbackImage; // Вернет картинку или null, если файла заглушки тоже нет
        }

        /// <summary>
        /// Пытается загрузить изображение через MemoryStream (не блокирует файл)
        /// </summary>
        private static Image TryLoadImage(string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        ms.Position = 0;
                        return Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static void FormatImageColumn(DataGridView grid, string imageColumnName, int width = 100)
        {
            if (grid.Columns.Contains(imageColumnName))
            {
                var imgCol = (DataGridViewImageColumn)grid.Columns[imageColumnName];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.Width = width;
                imgCol.HeaderText = "Фото";

                imgCol.DefaultCellStyle.NullValue = "Нет фото";
            }
        }
    }
}