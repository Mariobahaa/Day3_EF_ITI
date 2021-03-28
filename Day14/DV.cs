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
    public partial class DV : Form
    {
        public DV()
        {
            InitializeComponent();
        }


        TitleManager titleManager;
        //titleList titles;
        List<title> tcopy;
        PublisherManager publisherManager;
        List<Publisher> publisherList;
        BindingNavigator bindingNavigator;
        BindingSource BSrc;
        BindingSource PS;
      

        private void DV_Load(object sender, EventArgs e)
        {
            titleManager = new TitleManager();
            //titles = titleManager.selectAllTitles();
            tcopy = titleManager.GetAll();
            BSrc = new BindingSource(tcopy, "");
            

            publisherManager = new PublisherManager();
            publisherList = publisherManager.selectAllPublishers();
            PS = new BindingSource(publisherList, "");

            bindingNavigator = new BindingNavigator(true);
            this.Controls.Add(bindingNavigator);
            bindingNavigator.Dock = DockStyle.Bottom;
            bindingNavigator.BindingSource = BSrc;

            BindingSource b = new BindingSource();

            title_id.DataBindings.Add("Text", BSrc, "title_id", true);
            title.DataBindings.Add("Text", BSrc, "title", true);
            type.DataBindings.Add("Text", BSrc, "type", true);
            price.DataBindings.Add("Value", BSrc, "price", true);
            advance.DataBindings.Add("Value", BSrc, "advance", true);
            royalty.DataBindings.Add("Value", BSrc, "royalty", true);
            sales.DataBindings.Add("Value", BSrc, "ytd_sales", true);
            notes.DataBindings.Add("Text", BSrc, "notes", true);
            pubDate.DataBindings.Add("Value", BSrc, "pubdate", true);


            b.AddingNew += (sender, e) => b.AddNew();

            comboBox1.DataSource = PS; //new Data Table
            comboBox1.DisplayMember = "pub_name"; //pub name from the Binding Source from new Data Table
            comboBox1.ValueMember = "pub_id";
            comboBox1.DataBindings.Add("SelectedValue", BSrc, "pub_id", true);

          
          
            bindingNavigator.DeleteItem.MouseDown+= (sender, e) =>
            {
                var idtodelete = (string)title_id.Text;
                titleManager.Delete(idtodelete);
                MessageBox.Show("Item may not be deleted as it may be refrenced in other database tables");
                //BSrc.RemoveCurrent();
                //Trace.WriteLine(idtodelete);

            };
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BSrc.EndEdit();
            foreach (title dr in tcopy)
            {
                if (dr.State == EntityState.Added)
                {

                    titleManager.Add(dr);
                }
                else if (dr.State == EntityState.Modified)
                {
                 
                    titleManager.Update(dr);

                }
                dr.State = EntityState.Unchanged;
            }
        }
    }
}
