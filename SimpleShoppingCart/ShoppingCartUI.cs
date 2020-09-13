using SimpleShoppingCart.Controller;
using SimpleShoppingCart.Helper;
using SimpleShoppingCart.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleShoppingCart
{
    public partial class ShoppingCartUI : Form
    {
        // PROPERTIES OF THE SHOPPING CART
        int ID { get; set; }
        int CartID { get; set; }
        double Count { get; set; }
        int Offset { get; set; } = 0;
        int Span = 5;
        public IProductController ResProd { get; set; }
        public ICartController ResCart { get; set; }

        // Method to initialize the SQLConnection class
        public SQLConnection Conn { get; set; }

        // CONSTRUCTOR
        public ShoppingCartUI(IProductController resProd, ICartController resCart, SQLConnection conn)
        {
            InitializeComponent();
            this.ResProd = resProd;
            this.ResCart = resCart;
            this.Conn = conn;
        }

        // METHOD TO PERFORM WHEN THE FORM LOAD
        private void ShoppingCartUI_Load(object sender, EventArgs e)
        {
            // Open the connection
            Conn.OpenConnection();
            ResProd.GetAllProduct();
            this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
            this.dataGridView2.DataSource = ResCart.GetAllCart();
            // Close the connection
            Conn.CloseConnection();
        }

        // METHOD TO PERFORM WHEN THE FORM IS CLOSING
        private void ShoppingCartUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the connection
            Conn.OpenConnection();
            ResCart.ClearCart();
            // Close the connection
            Conn.CloseConnection();
        }

        // METHOD TO ADD PRODUCT
        private void button1_Click(object sender, EventArgs e)
        {
            decimal.TryParse(textBox2.Text, out decimal d);

            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (d > 0 && decimal.TryParse(textBox2.Text, out decimal _)){
                        // Method to add to the Product table
                        ResProd.AddProduct(textBox1.Text, d);
                        // Method to Populate the Data Grid View
                        this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
                        MessageBox.Show("Product Added Successfully");
                    } else
                    {
                        MessageBox.Show("Enter a Positive Value");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fill all the Boxes available");
            }

            // Change the text to empty string
            textBox1.Text = "";
            textBox2.Text = "";
        }

        // METHOD TO EDIT AND UPDATE PRODUCT
        private void button2_Click(object sender, EventArgs e)
        {
            decimal.TryParse(textBox3.Text, out decimal d);

            try
            {
                if (textBox3.Text != "" && textBox4.Text != "")
                {
                    if(d > 0 && decimal.TryParse(textBox3.Text, out decimal _))
                    {
                        // Method to edit to the Product table
                        ResProd.EditProduct(ID, textBox4.Text, d);
                        // Method to Populate the Data Grid View
                        this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
                        MessageBox.Show("Product Updated Successfully");
                    } else
                    {
                        MessageBox.Show("Invalid Details");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill all the Boxes Available");
            }

            // Change the Text Box Data
            textBox3.Text = "";
            textBox4.Text = "";
        }

        // METHOD TO REMOVE FROM PRODUCT
        private void button3_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                // Method to remove from the Product table
                ResProd.RemoveProduct(ID);
                // Method to Populate the Data Grid View
                this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
                this.dataGridView2.DataSource = ResCart.GetAllCart();

                MessageBox.Show("Product Deleted Successfully");
            }
            else
            {
                MessageBox.Show("Invalid Details");
            }
        }

        // METHOD TO ADD TO CART
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Method to add to the Cart table
                ResCart.AddCart(ID, (int)numericUpDown1.Value);
                // Method to Populate the Data Grid View
                this.dataGridView2.DataSource = ResCart.GetAllCart();
                MessageBox.Show("Product Added to Cart Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid Product Details");
            }

            // Change the Numeric Value back to default 1;
            numericUpDown1.Value = 1;
        }

        // METHOD TO REMOVE FROM CART
        private void button5_Click(object sender, EventArgs e)
        {
            if(CartID > 0)
            {
                // Method to remove from the Cart table
                ResCart.RemoveCart(CartID);
                // Method to Populate the Cart Data Grid View
                this.dataGridView2.DataSource = ResCart.GetAllCart();
                MessageBox.Show("Cart Item Deleted Successfully");
            }
            else { 
                MessageBox.Show("Invalid Details");
            }
        }

        // METHOD TO GET ID ON THE CELL CLICKED EVENT, ON THE PRODUCT DATAGRIDVIEW
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductId"].FormattedValue.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Wrong Field Data Inputted or Clicked");
            }
        }

        // METHOD TO GET ID ON THE CELL CLICKED EVENT, ON THE CART DATAGRIDVIEW
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView2.CurrentRow.Selected = true;
                    CartID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Wrong Field Data Inputted or Clicked");
            }       
        }

        // METHOD TO FILTER THE PRODUCT BY THE PRICE PROVIDED
        private void button8_Click(object sender, EventArgs e)
        {
            decimal.TryParse(textBox5.Text, out decimal d);
            try
            {
                if(textBox5.Text != "")
                {
                    if (d > 0 && decimal.TryParse(textBox5.Text, out decimal _))
                    {
                        // Method to filter the product list by the price inputted
                        this.dataGridView1.DataSource = ResProd.FilterProductByPrice(d);
                        textBox5.Text = "";
                    }
                }

                else
                {
                    // Method to populate back the product list by its offset
                    this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
                    textBox5.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Details");
            }
        }

        // METHOD TO FILTER THE PRODUCT BY THE NAME PROVIDED
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    // Method to filter the product list by the name inputted
                    this.dataGridView1.DataSource = ResProd.FilterProductByProductName(textBox5.Text);
                    textBox5.Text = "";
                } else
                {
                    // Method to populate back the product list by its offset
                    this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
                    textBox5.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Details                                                                                                                                                                                                                                ");
            }
        }

        // METHOD TO REFRESH THE PRODUCT DATAGRIDVIEW
        private void button10_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
        }

        // METHOD TO GO NEXT OF PRODUCTLIST
        private void button7_Click(object sender, EventArgs e)
        {
            Offset += 5;
            var list = ResProd.GetAllProductByOffSet(Offset, Span);
            
            if(list.Count == 0)
            {
                Offset -= 5;
                list = ResProd.GetAllProductByOffSet(Offset, Span);
            }
            this.dataGridView1.DataSource = list;
        }

        // METHOD TO GO PREVIOUS OF PRODUCTLIST
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Offset -= 5;
                this.dataGridView1.DataSource = ResProd.GetAllProductByOffSet(Offset, Span);
            }
            catch (Exception)
            {
                Offset += 5;
            }
        }

        
    }
}
