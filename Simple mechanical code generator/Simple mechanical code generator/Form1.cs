using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Simple_mechanical_code_generator
{
    public partial class Form1 : Form
    {
        private List<decimal[]> myIntLists = new List<decimal[]>();
        private bool[] curr;
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("NOP");
            listBox1.Items.Add("BRANCH");
            listBox1.Items.Add("LOAD");
            listBox1.Items.Add("STORE");
            listBox1.Items.Add("ADD");
            listBox1.Items.Add("MULTIPLY");
            listBox1.Items.Add("COMPLEMENT");
            listBox1.Items.Add("SHIFT");
            listBox1.Items.Add("ROTATE");
            listBox1.Items.Add("HALT");
            listBox3.Items.Add("Always");
            listBox3.Items.Add("Carry");
            listBox3.Items.Add("Even");
            listBox3.Items.Add("Parity");
            listBox3.Items.Add("Zero");
            listBox3.Items.Add("Negative");
            curr = new bool[32];
            oneside_cond_code();
            Forced_withdrawal_prohibition();

        }
        private void oneside_cond_code()
        {
            imm.Enabled = false;
            des_imm.Enabled = false;
        }
        private void Forced_withdrawal_prohibition()
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void printcurr()
        {
            string x="";
            int oz = 0;
            for (int i = 32; i>=1; i--)
            {
                oz=(curr[i-1])?1:0;
                if (i % 4==0)
                {
                    x = x + "_";
                    x = x + oz.ToString();
                    
                }
                else
                {
                    x = x + oz.ToString();
                }
            }
            label3.Text = x;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void l2change (){
            label2.Text = listBox1.SelectedIndex.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "DefaultOutputName.txt";

            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)

            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

                for (int i = 0; i < listBox2.Items.Count; i++)

                {

                    writer.WriteLine(listBox2.Items[i].ToString());

                }
                //writer.WriteLine("OK");
                writer.Dispose();

                writer.Close();

            }
        
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Forced_withdrawal_prohibition();
            disable_cond_code(false);
            oneside_cond_code();
            l2change();
            int idx= listBox1.SelectedIndex;
            int pa=0;
            int[] hay;
            hay = new int[4];
            
            for (int i = 0; i < 4; i++)
            {
                curr[28 + i] = false;
                pa =idx%2;
                idx = (idx - pa) / 2;
                curr[28+i] = (pa==1)?true:false;
            }
op();
            printcurr();
            

        }
        private void zc(int a,int b,bool yn)
        {
            for(int i = a; i <= b; i++)
            {
                curr[i] = yn;
            }
        }
        private void disable_cond_code(bool yn)
        {
            syt_reg.Enabled = (yn) ? false : true;
            des_imm.Enabled = (yn) ? false : true;
            des_reg.Enabled = (yn) ? false : true;
            imm.Enabled = (yn) ? false : true;
        }
        private void clr_loc(int/*1=sou;2=des*/ loc)
        {
            switch (loc)
            {
                case 2:
                for(int i = 0; i < 12; i++)
            {
                curr[i] = false;
            }
                    break;
                case 1:
                    for (int i = 12; i < 24; i++)
                    {
                        curr[i] = false;
                    }
                    break;
                default:
                    break;
            }
            
        }
        private void des()
        {
            int ct = 0;
            String result = Convert.ToString(Convert.ToInt32(numericUpDown2.Value.ToString()/*number(in string format)*/, 10/* fromBase*/), 2/*toBase*/);
            clr_loc(/*1=sou;2=des*/ 2);
            
            for (int i =0; i< result.Length; i++)
            {
                curr[i] = (result[result.Length - 1 - i]=='1')?true:false;
                ct++;
            }
            
        }
        private void soc()
        {
            int ct = 0;
            String result = Convert.ToString(Convert.ToInt32(numericUpDown1.Value.ToString()/*number(in string format)*/, 10/* fromBase*/), 2/*toBase*/);
            clr_loc(/*1=sou;2=des*/ 1);

            for (int i = 0; i < result.Length; i++)
            {
                curr[i+12] = (result[result.Length - 1 - i] == '1') ? true : false;
                ct++;
            }
            
        }
        private void open_num_up_down()
        {
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;

        }
        private void op()
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    disable_cond_code(true);
                    listBox3.Enabled = false;
                    zc(0, 27, false);
                    break;
                case 1:
                    listBox3.Enabled = true;
                    button4.Enabled = true;
                    numericUpDown1.Enabled = false;
                    disable_cond_code(true);
                    button3.Enabled = true;
                    break;
                case 2:
                    open_num_up_down();
                    disable_cond_code(false);
                    listBox3.Enabled = false;
                    oneside_cond_code();
                    SDTtoC();
                    break;
                case 3:
                    open_num_up_down();
                    disable_cond_code(false);
                    listBox3.Enabled = false;
                    oneside_cond_code();
                    SDTtoC();
                    break;
                case 4:
                    open_num_up_down();
                    disable_cond_code(false);
                    listBox3.Enabled = false;
                    oneside_cond_code();
                    SDTtoC();
                    break;
                case 5:
                    open_num_up_down();
                    disable_cond_code(false);
                    listBox3.Enabled = false;
                    oneside_cond_code();
                    SDTtoC();
                    break;
                case 6:
                    open_num_up_down();
                    disable_cond_code(false);
                    listBox3.Enabled = false;
                    oneside_cond_code();
                    SDTtoC();
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    disable_cond_code(true);
                    listBox3.Enabled = false;
                    zc(0, 27, false);
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printcurr();
            listBox2.Items.Add(label3.Text);
            sv(0/*add=0;plugin=1*/);
        }
        private void sv(int /*add=0;plugin=1*/ip)
        {
            decimal[] oup;
            oup = new decimal[12];
            oup[0] = (listBox1.Enabled) ? 1 : 0;
            oup[1] = listBox1.SelectedIndex;
            oup[2] = (syt_reg.Enabled) ? 1 : 0;
            oup[3] = (imm.Enabled) ? 1 : 0;
            oup[4] = (des_reg.Enabled) ? 1 : 0;
            oup[5] = (des_imm.Enabled) ? 1 : 0;
            oup[6] = (listBox3.Enabled) ? 1 : 0;
            oup[7] = listBox3.SelectedIndex;
            oup[8] = (numericUpDown1.Enabled) ? 1 : 0;
            oup[9] = numericUpDown1.Value;
            oup[10] = (numericUpDown2.Enabled) ? 1 : 0;
            oup[11] = numericUpDown2.Value;
            if (ip == 0)
            {
                myIntLists.Add(oup);
            }
            else
            {
                myIntLists.Insert(listBox2.SelectedIndex,oup);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void syt_reg_Click(object sender, EventArgs e)
        {
            imm.Enabled = true;
            syt_reg.Enabled = false;
            SDTtoC();
        }

        private void imm_Click(object sender, EventArgs e)
        {
            syt_reg.Enabled = true;
            imm.Enabled = false;
            SDTtoC();
        }

        private void des_reg_Click(object sender, EventArgs e)
        {
            des_imm.Enabled = true;
            des_reg.Enabled = false;
            SDTtoC();
        }

        private void des_imm_Click(object sender, EventArgs e)
        {
            des_reg.Enabled = true;
            des_imm.Enabled = false;
            SDTtoC();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.Enabled)
            {
                int dec_bin = 3;
                String result = Convert.ToString(Convert.ToInt32(listBox3.SelectedIndex.ToString()/*number(in string format)*/, 10/* fromBase*/), 2/*toBase*/);
                label7.Text = (dec_bin==0)? result/*0*/ : (dec_bin == 1) ? result.Length.ToString()/*1*/ : listBox3.SelectedIndex.ToString()/*2*/;
                switch (result.Length)
                {
                    case 0:
                        curr[24] = false;
                        curr[25] = false;
                        curr[26] = false;
                        curr[27] = false;
                        break;
                    case 1:
                        curr[24] = (result[0]=='1')?true:false;
                        curr[25] = false;
                        curr[26] = false;
                        curr[27] = false;
                        break;
                    case 2:
                        curr[24] = (result[1] == '1') ? true : false;
                        curr[25] = (result[0] == '1') ? true : false;
                        curr[26] = false;
                        curr[27] = false;
                        break;
                    case 3:
                        curr[24] = (result[2] == '1') ? true : false;
                        curr[25] = (result[1] == '1') ? true : false;
                        curr[26] = (result[0] == '1') ? true : false;
                        curr[27] = false;
                        break;
                    default:
                        label7.Text = "ERROR!";
                        break;
                }
                printcurr();
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            des();

            printcurr();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            soc();
printcurr();
        }
        private void SDTtoC()
        {
            curr[27]=(syt_reg.Enabled)?false:true;
            curr[26] = (des_reg.Enabled) ? false : true;
            curr[25] = curr[24] = false;
            printcurr();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal[] oup;
            oup = new decimal[12];
            label8.Text = listBox2.SelectedIndex.ToString() + myIntLists.Count.ToString();
            if ((listBox2.SelectedIndex >= 0) && (listBox2.SelectedIndex< myIntLists.Count))
            {
                oup = myIntLists[listBox2.SelectedIndex];
                listBox1.Enabled = (oup[0] == 1) ? true : false;
                listBox1.SelectedIndex = Decimal.ToInt32(oup[1]);
                syt_reg.Enabled = (oup[2] == 1) ? true : false;
                imm.Enabled = (oup[3] == 1) ? true : false;
                des_reg.Enabled = (oup[4] == 1) ? true : false;
                des_imm.Enabled = (oup[5] == 1) ? true : false;
                listBox3.Enabled = (oup[6] == 1) ? true : false;
                listBox3.SelectedIndex = Decimal.ToInt32(oup[7]);
                numericUpDown1.Enabled = (oup[8] == 1) ? true : false;
                numericUpDown1.Value = oup[9];
                numericUpDown2.Enabled = (oup[10] == 1) ? true : false;
                numericUpDown2.Value = oup[11];
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            printcurr();
            listBox2.Items.Insert(listBox2.SelectedIndex, label3.Text);
               
            sv(1/*add=0;plugin=1*/);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int tmp = listBox2.SelectedIndex;
            myIntLists.RemoveAt(tmp);
            listBox2.Items.RemoveAt(tmp);
            listBox2.SelectedIndex = 0;

        }
        private int sr = 32;
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            frsr();
        }
        private void frsr()
        {
            sr = Decimal.ToInt32(numericUpDown3.Value);
            label9.Text = sr.ToString();
            Isr();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            button9.Enabled = true;
            button8.Enabled = false;
            frsr();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button8.Enabled = true;
            button9.Enabled = false;

            frsr();

        }

        private void Isr()
        {
            
            string result = Convert.ToString(Convert.ToInt32(sr.ToString()/*number(in string format)*/, 10/* fromBase*/), 2/*toBase*/);
            if (button8.Enabled)
            {
                int[] r2;
                r2 = new int[result.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    r2[result.Length - 1 - i] = (result[result.Length - 1 - i] == '1') ? 0 : 1;
                }
                int k = 0;
                while (r2[k] != 0)
                {
                    r2[result.Length - 1 - k] = (result[result.Length - 1 - k] == '1') ? 0 : 1;
                    k++;
                }
                r2[result.Length - 1 - k] = (result[result.Length - 1 - k] == '1') ? 0 : 1;
                clr_loc(/*1=sou;2=des*/ 1);

                for (int i = 0; i < r2.Length; i++)
                {
                    curr[i + 12] = (r2[r2.Length - 1 - i] == 1) ? true : false;

                }
            }
            else
            {
                
                clr_loc(/*1=sou;2=des*/ 1);

                for (int i = 0; i < result.Length; i++)
                {
                    curr[i + 12] = (result[result.Length - 1 - i] == '1') ? true : false;
                    
                }
            }
            printcurr();

        }
    }

}
