using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace EL_GAMALYA_algoritm
{
    class Algoritm
    {
        public int kk;
        private int Rand()
        {
            Random random = new Random();
            return random.Next();
        }

        private static bool isSimple(int N)
        {
            for (int i = 2; i < (int)(N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }

        private int power(int a, int b, int n) // a^b mod n 
        {
            int tmp = a;
            int sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
                {
                    sum += tmp;
                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }
                tmp = sum;
            }
            return tmp;
        }

        private int pow(int a, int b) // a^b
        {
            int tmp = a;
            int sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
                {
                    sum += tmp;
                }
                tmp = sum;
            }
            return tmp;
        }

        private int mul(int a, int b, int n) // a*b mod n 
        {
            int sum = 0;
            for (int i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                {
                    sum -= n;
                }
            }
            return sum;
        }


        public void cryptintcheck(int p, int g, int x, int instr, TextBox tbinfo, TextBox tbcipher)
        {
           
                int y = power(g, x, p);
                int k = 0;
                int a, b;
                int m = instr;
                tbinfo.Text += " public key (p,g,y) = (" + p + "," + g + "," + y + ")" + " private key x = " + x;

            m1: k = Rand() % (p - 3) + 2; // 1 < k < (p-1)
                for (int j = k; j > 1; j--)
                {
                    if (k % j == 0 && (p - 1) % j == 0)
                        goto m1;
                }
               

                if (m > 0)
                {
                    a = power(g, k, p);
                    // b = mul(power(y, k, p), m, p); //(y^k)*m mod p
                    b = (power(y, k, p) * m) % p;
                    // b = mul(pow(y, k), m, p);
                    //if (a != 0 && b != 0)
                    tbcipher.Text += a + " " + b + " ";
                    //else goto m1;
                }


                tbinfo.Text += " k= " + k;

        }

        public void crypt(int p, int g, int x, string instr, TextBox tbinfo, TextBox tbcipher)
        {
           //System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            //byte[] temp = Encoding.Unicode.GetBytes(instr); //enc.GetBytes(instr);
            if (isSimple(p) == true )
            {
                int y = power(g, x, p);
                int k = 0;
                int a, b;
                tbinfo.Text += " public key (p,g,y) = (" + p + "," + g + "," + y + ")" + " private key x = " + x;

                m1: k = Rand() % (p - 3) + 2; // 1 < k < (p-1)
                for (int j = k; j > 1; j--)
                {
                    if (k % j == 0 && (p - 1) % j == 0)
                        goto m1;
                }
                //k = 3;
               // tbinfo.Text += " " + instr;
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                byte[] temp = enc.GetBytes(instr);          

                if (instr.Length > 0)
                {
                    /*char[] temp = new char[instr.Length - 1];
                    temp = instr.ToCharArray();*/

                    foreach (byte value in temp)
                    {
                             a = power(g, k, p);
                             b = (power(y, k, p) * value) % p;
                                tbcipher.Text += a + " " + b + " ";
                        }
                    }
                

                tbinfo.Text += " k= " + k;
                kk = k;
          
            }
            else
                MessageBox.Show("Enter another P/G");
        }


        public void decryptintcheck(int p, int x, TextBox tb,/*string instr,*/TextBox tbinfo)
        {
            string s = tb.Text;
            string[] arrstring = new string[1];
            if (s != "")
            {
                arrstring = s.Split(' ');
            }
            int[] mas = new int[arrstring.Length - 1];
            for (int i = 0; i < arrstring.Length - 1; i++)
            {
                mas[i] = Convert.ToInt32(arrstring[i]);
            }

            for (int i = 0; i < mas.Length; i++)
            {
                tbinfo.Text += "mas" + mas[i];
            }

            int a, b;
            int decM;
            Int32[] deM = new int[mas.Length];
            Int32[] dem = new int[deM.Length / 2];
            char m;
            string tmps;
            for (int i = 0, j = 0; i < mas.Length; i += 2, j++)
            {
                a = mas[i];
                b = mas[i + 1];
                tbinfo.Text += "a=" + a+ "b=" + b;
                deM[j] = (b * (power(a, p - 1 - x,p))) % p;
                tbinfo.Text += "__"+deM[j]; 
    
            }

        }

        public void decrypt(int p, int x, int g,TextBox tb,/*string instr,*/TextBox tbinfo)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            string s = tb.Text;
            string[] arrstring = new string[1];
            if (s != "")
            {
                arrstring = s.Split(' ');
            }
            Int32[] mas = new Int32[arrstring.Length - 1];
            for (int i = 0; i < arrstring.Length - 1; i++)
            {
                mas[i] = Convert.ToInt32(arrstring[i]);
            }
            byte[] arrbyte = new byte[mas.Length];


            int a, b;
            a = power(g, kk, p);
            b = power(a, p - 1 - x, p) % p;

            int j = 0;
            foreach (int i in mas)
            {
                byte decryptedValue = (byte)((i * b) % p);
                arrbyte[j] = decryptedValue;
                j++;

            }
string result = System.Text.Encoding.UTF8.GetString(arrbyte);
             String pattern = "|";
      String[] elements = Regex.Split(result, pattern);
       for (int i = 0 ; i <= elements.Length -1; i+=2)
          tbinfo.Text += elements[i];

        }
         

        }
    }
  
