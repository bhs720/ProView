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
        static readonly string jobStoreFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"ProView", "Jobs");
        static readonly string jobFileExtension = ".xml";
        static JobManager()
        {
            try
            {
                if (!Directory.Exists(jobStoreFolder))
                    Directory.CreateDirectory(jobStoreFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem with Job Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<string> GetStoredJobs()
        {
            var jobNames = new List<string>();
            try
            {
                foreach (var file in Directory.EnumerateFiles(jobStoreFolder))
                    jobNames.Add(Path.GetFileNameWithoutExtension(file));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem getting stored jobs", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Problem loading job", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Problem saving job", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Problem exporting job file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
