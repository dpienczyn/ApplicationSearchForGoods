using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            //MessageBox.Show(two);

        }

        public string GetNumbers()
        {
            string number = textBox1.Text;
            return number;
        }

        public string TextBoxWrite(string number)
        {
            if (number == "Symbol..")
            {
                number = "";
                //number.ForeColor = Color.Black;
            }
            return number;
        }

        public void TableDataRow(DataTable dtbl)
        {
            dataGridView1.AutoGenerateColumns = false;

            foreach (DataRow row in dtbl.Rows)
            {
                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = row["Dostawca"];
                dataGridView1.Rows[i].Cells[1].Value = row["NumerDokumentu"];
                dataGridView1.Rows[i].Cells[2].Value = row["NumerFaktury"];
                dataGridView1.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy";
                dataGridView1.Rows[i].Cells[3].Value = row["DataWystawienia"];
                dataGridView1.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy";
                dataGridView1.Rows[i].Cells[4].Value = row["DataOtrzymania"];
                dataGridView1.Columns[5].DefaultCellStyle.Format = "0.00##";
                dataGridView1.Rows[i].Cells[5].Value = row["CenaWaluta"];
                dataGridView1.Rows[i].Cells[6].Value = row["Waluta"];

            }
        }


        public void ClearDataGirdView()
        {
            dataGridView1.Rows.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ClearDataGirdView();
            string one = GetNumbers();
            string number = TextBoxWrite(one);
            DataBase dataBase = new DataBase();
            DataTable t = dataBase.InvoiceData(number);
            TableDataRow(t);
        }

        
    }
}
