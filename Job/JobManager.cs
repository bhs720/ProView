using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ProView
{
    public static class JobManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static readonly string jobStoreFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProView", "Jobs");
        static readonly string jobFileExtension = ".xml";

        static JobManager()
        {
            try
            {
                Directory.CreateDirectory(jobStoreFolder);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to create folder: {@jobStoreFolder}", jobStoreFolder);
                MessageBox.Show($"An error occurred while attempting to create the folder:{Environment.NewLine}{Environment.NewLine}{jobStoreFolder}{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<string> GetStoredJobs()
        {
            var jobNames = new List<string>();
            try
            {
                foreach (var file in Directory.EnumerateFiles(jobStoreFolder))
                {
                    jobNames.Add(Path.GetFileNameWithoutExtension(file));
                }
                    
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to enumerate files in {@jobStoreFolder}", jobStoreFolder);
                MessageBox.Show($"An error occurred while attempting to find files in:{Environment.NewLine}{Environment.NewLine}{jobStoreFolder}{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return jobNames;
        }

        public static List<Field> LoadJob(string jobName)
        {
            List<Field> fields = null;
            try
            {
                var xmlReader = new XmlSerializer(typeof(List<Field>));
                var file = Path.Combine(jobStoreFolder, jobName + jobFileExtension);
                using (var txtReader = new StreamReader(file))
                {
                    fields = xmlReader.Deserialize(txtReader) as List<Field>;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to deserialize \"{@jobStoreFolder}\\{@jobName}.{@jobFileExtension}\"", jobStoreFolder, jobName, jobFileExtension);
                MessageBox.Show($"An error occurred while attempting to load the job \"{jobName}\"{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fields;
        }

        public static void SaveJob(string jobName, List<Field> fields)
        {
            try
            {
                var xmlWriter = new XmlSerializer(typeof(List<Field>));
                var file = Path.Combine(jobStoreFolder, jobName + jobFileExtension);
                using (var txtWriter = new StreamWriter(file))
                {
                    xmlWriter.Serialize(txtWriter, fields);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to serialize \"{@jobStoreFolder}\\{@jobName}.{@jobFileExtension}\"", jobStoreFolder, jobName, jobFileExtension);
                MessageBox.Show($"An error occurred while attempting to save the job \"{jobName}\"{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteJob(string jobName)
        {
            try
            {
                var file = Path.Combine(jobStoreFolder, jobName + jobFileExtension);
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to delete file \"{@jobStoreFolder}\\{@jobName}.{@jobFileExtension}\"", jobStoreFolder, jobName, jobFileExtension);
                MessageBox.Show(ex.Message, "Problem deleting job", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ImportFile(string srcFile)
        {
            try
            {
                var dstFile = Path.Combine(jobStoreFolder, Path.GetFileName(srcFile));
                File.Copy(srcFile, dstFile);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to copy file from \"{@srcFile}\" to \"{@jobStoreFolder}\"", srcFile, jobStoreFolder);
                MessageBox.Show(ex.Message, "Problem importing job file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportFile(string jobName, string dstFile)
        {
            try
            {
                var srcFile = Path.Combine(jobStoreFolder, jobName + jobFileExtension);
                File.Copy(srcFile, dstFile);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to copy file from \"{@jobStoreFolder}\\{@jobName}.{@jobFileExtension}\" to \"{@dstFile}\"", jobStoreFolder, jobName, jobFileExtension, dstFile);
                MessageBox.Show(ex.Message, "Problem exporting job file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
