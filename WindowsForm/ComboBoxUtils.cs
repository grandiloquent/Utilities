using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Utilities.WindowsForm
{
    public static class ComboBoxUtils
    {

        public static void LoadItemsFromFile(this ToolStripComboBox box, string fileName)
        {

            var p = fileName.GetApplicationPath();

            if (p.IsFile())
            {
                var ls = p.ReadFileAllLines().Where(i => !i.IsNullOrWhiteSpace()).OrderBy(i => i);

                box.Items.Clear();
                box.Items.AddRange(ls.ToArray());
            }

        }
        public static void AddItemsToFile(this ToolStripComboBox box, string fileName, string value)
        {

            var p = fileName.GetApplicationPath();

            if (p.IsFile())
            {
                var ls = p.ReadFileAllLines().ToList();

                if (ls.Contains(value)) return;
                else
                {
                    ls.Add(value);
                }

                using (var writer = new StreamWriter(p, true, FileUtils.UTF8_ENCODING))
                {
                    writer.Write(value);
                    writer.Write(Environment.NewLine);
                }

                box.Items.Clear();
                box.Items.AddRange(ls.ToArray());
            }
            else
            {
                value.StringToFile(p);
                box.Items.Add(value);
                
            }

        }
    }
}
