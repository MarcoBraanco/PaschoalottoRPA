using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PaschoalottoRPA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)  
        {
            DatabaseActions db = new DatabaseActions();

            db.OpenConnection();

            SeleniumActions selenium = new SeleniumActions();

            var data = selenium.GetDataFromWebsite();

            if (data.Item6 != "Success")
            {
                MessageBox.Show("Erro ao realizar extraçao de dados");
            }


            db.InsetDataToTable(data.wpmData, data.keyStrokesData, data.accuracyData, data.correctWordsData, data.wrongWordsData);

        }
    }
}
