using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DP.EnitiyManagers;
using DP.Entities;


namespace DP
{
    public partial class GV : Form
    {
        public GV()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        TitleManager titleManager;
        //titleList titles;
        List<title> tcopy;
        PublisherManager publisherManager;
        List<Publisher> publisherList;

        private void GV_Load(object sender, EventArgs e)
        {
            titleManager = new TitleManager();
            //titles = titleManager.selectAllTitles();
            tcopy = titleManager.GetAll();
            var source = new BindingSource(tcopy, "");
            dataGridView1.DataSource = source;

            publisherManager = new PublisherManager();
            publisherList = publisherManager.selectAllPublishers();
            var PS = new BindingSource(publisherList, "");

            DataGridViewComboBoxColumn ck = new DataGridViewComboBoxColumn();
            ck.HeaderText = "Publisher";
            ck.DisplayMember = "pub_name";
            ck.ValueMember = "pub_id";
            ck.DataSource = PS;

            ck.DataPropertyName = "pub_id";
            dataGridView1.Columns.Add(ck);

            dataGridView1.Columns["pub_id"].Visible = false;

            
            dataGridView1.UserDeletingRow+= (args, e) =>
            {
                var idToDelete = (String)e.Row.Cells["Title_id"].Value;
                titleManager.Delete(idToDelete);
                
            };
        }

        private void btnDGVSave_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            foreach(title dr in tcopy)
            {
                if (dr.State == EntityState.Added)
                {
                    titleManager.Add(dr);
                }
                else if(dr.State == EntityState.Modified)
                {
                  
                    titleManager.Update(dr);

                }
                dr.State = EntityState.Unchanged;

            }
        }
    }
}
