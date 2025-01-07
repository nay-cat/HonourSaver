using LSLib.LS;
using LSLib.LS.Enums;
using System.Xml.Linq;
using ImageMagick;
using LSLib.VirtualTextures;

namespace HonourSaver
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            // la ruta default de baldurs debería ser esta
            // anyways, deje que se pueda colocar cualquiera en la textbox x sia caso
            pathText.Text = @"%LOCALAPPDATA%\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\";
        }

        private void loadSaves_Click(object sender, EventArgs e)
        {
            string filePath = pathText.Text + "profile8.lsf";

            try
            {
                filePath = Environment.ExpandEnvironmentVariables(filePath);

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Incorrect path", "Error");
                    return;
                }

                // backup 
                string backupFilePath = filePath + ".backup";
                File.Copy(filePath, backupFilePath, overwrite: true);

                string lsxFilePath = Path.ChangeExtension(filePath, ".lsx");
                ConvertLsfToLsx(filePath, lsxFilePath);

                LoadGuidsToComboBox(lsxFilePath);
                MessageBox.Show("Saves loaded", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on converting: {ex.Message}", "Error");
            }
        }




        private void RunSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pathText = @"%LOCALAPPDATA%\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\";
            string savegamesPath = Path.Combine(Environment.ExpandEnvironmentVariables(pathText), "Savegames", "Story");

            string selectedGuid = runSelected.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGuid))
            {
                MessageBox.Show("Select a save");
                return;
            }

            if (!Directory.Exists(savegamesPath))
            {
                MessageBox.Show("Image not found");
                return;
            }

            string targetDirectory = Directory.GetDirectories(savegamesPath)
                .FirstOrDefault(dir => dir.Contains(selectedGuid));

            if (string.IsNullOrEmpty(targetDirectory))
            {
                MessageBox.Show("Path not found");
                return;
            }

            string webpFile = Directory.GetFiles(targetDirectory, "*.webp").FirstOrDefault();
            if (string.IsNullOrEmpty(webpFile))
            {
                MessageBox.Show("WebP not found");
                return;
            }

            try
            {
                string pngPath = Path.ChangeExtension(webpFile, ".png");
                using (var image = new MagickImage(webpFile))
                {
                    image.Write(pngPath);
                }

                savePicture.ImageLocation = pngPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing {ex.Message}");
            }
        }

        private void lsfLogic_Click(object sender, EventArgs e)
        {
            string selectedGuid = runSelected.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGuid))
            {
                MessageBox.Show("Save not selected.");
                return;
            }

            string filePath = pathText.Text + "profile8.lsx";
            string lsfFilePath = pathText.Text + "profile8.lsf";

            try
            {
                filePath = Environment.ExpandEnvironmentVariables(filePath);
                lsfFilePath = Environment.ExpandEnvironmentVariables(lsfFilePath);

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("LSX not found.", "Error");
                    return;
                }

                // cargar el archivo lsx y eliminar el nodo de la GUID seleccionada
                XDocument doc = XDocument.Load(filePath);

                var nodesToRemove = doc.Descendants("node")
                                       .Where(node => (string)node.Attribute("id") == "DisabledSingleSaveSessions")
                                       .Descendants("node")
                                       .Where(node => node.Descendants("attribute")
                                                          .Any(attr => (string)attr.Attribute("type") == "guid" &&
                                                                       (string)attr.Attribute("value") == selectedGuid))
                                       .ToList();

                if (nodesToRemove.Count == 0)
                {
                    MessageBox.Show("Save not found.", "Información");
                    return;
                }

                foreach (var node in nodesToRemove)
                {
                    node.Remove();
                }

                // guardar lsx
                doc.Save(filePath);

                // reconvertir a lsf
                ConvertLsxToLsf(filePath, lsfFilePath);

                MessageBox.Show("Honour Mode reactivated.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on processing: {ex.Message}", "Error");
            }
        }

        private void ConvertLsfToLsx(string lsfFilePath, string lsxFilePath)
        {
            using (var inputStream = new FileStream(lsfFilePath, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(lsxFilePath, FileMode.Create, FileAccess.Write))
            {
                var reader = new LSFReader(inputStream);
                var package = reader.Read();

                var writer = new LSXWriter(outputStream)
                {
                    PrettyPrint = true,
                    Version = LSXVersion.V4
                };
                writer.Write(package);
            }
        }

        private void ConvertLsxToLsf(string lsxFilePath, string lsfFilePath)
        {
            using (var inputStream = new FileStream(lsxFilePath, FileMode.Open, FileAccess.Read))
            using (var outputStream = new FileStream(lsfFilePath, FileMode.Create, FileAccess.Write))
            {
                var reader = new LSXReader(inputStream);
                var package = reader.Read();

                var writer = new LSFWriter(outputStream);
                writer.Write(package);
            }
        }

        private void LoadGuidsToComboBox(string lsxFilePath)
        {
            XDocument doc = XDocument.Load(lsxFilePath);

            // <node id="DisabledSingleSaveSessions">
            var guids = doc.Descendants("node")
                           .Where(node => (string)node.Attribute("id") == "DisabledSingleSaveSessions")
                           .Descendants("attribute")
                           .Where(attr => (string)attr.Attribute("type") == "guid")
                           .Select(attr => (string)attr.Attribute("value"))
                           .Distinct() 
                           .ToList();

            runSelected.Items.Clear();
            runSelected.Items.AddRange(guids.ToArray());
        }

        private void ConvertWebPToPng(string webpPath, string pngPath)
        {
            using (var webpImage = System.Drawing.Image.FromFile(webpPath))
            {
                webpImage.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
