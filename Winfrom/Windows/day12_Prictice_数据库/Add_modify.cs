using day12_Prictice_数据库.工具类;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace day12_Prictice_数据库
{
    public partial class Add_modify : Form
    {
        //需要修改的问题: 1 列的索引是固定的
        //2. 更新中解决日期类型 与 bit类型转换 bit 需要设置为 0与1 int  日期需要单引号括起来

        //默认false 为修改
        public Add_modify(int index, string btnText = "修改", bool flag = false)
        {
            InitializeComponent();
            InitControls();
            if (btnText == "修改")
            {
                button1.Text = btnText;
                sqlCommand(index);
            }
            else if (btnText == "添加")
            {
                button1.Text = "添加";
            }
        }
        private void InitControls()
        {
            comboBox1.Items.Add("男");
            comboBox1.Items.Add("女");
        }
        private void sqlCommand(int index)
        {
            //声明参数变量
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int),

            };
            sqlParameters[0].Value = index;


            #region 调用工具类 查询
            DataSet dataSet = DbConnectionHelper.GetReader(DbConnectionHelper.GetConnection(), "select Id,StuName,StuAge,StuSex ,StuBirthday from Students where Id=@Id", sqlParameters);
            #endregion
            if (dataSet.Tables[0].Rows.Count > 0 && dataSet != null)
            {
                textBox1.Text = dataSet.Tables[0].Rows[0]["Id"].ToString();
                textBox3.Text = dataSet.Tables[0].Rows[0]["StuName"].ToString();
                numericUpDown1.Text = dataSet.Tables[0].Rows[0]["StuAge"].ToString();
                if (DBNull.Value != dataSet.Tables[0].Rows[0]["StuSex"])
                {

                    comboBox1.Text = (bool)dataSet.Tables[0].Rows[0]["StuSex"] ? "男" : "女";
                }
                if (DBNull.Value != dataSet.Tables[0].Rows[0]["StuBirthday"])
                {
                    dateTimePicker1.Value = (DateTime)dataSet.Tables[0].Rows[0]["StuBirthday"];
                }
            }


        }

        private void Add_modify_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

        }

        /// <summary>
        /// 添加/修改 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "添加")
            {

                sql(-1, "添加");
            }
            else if (button1.Text == "修改")
            {
                sql((Convert.ToInt32(textBox1.Text)), "修改");
            }
        }
        #region 添加/修改
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="index"></param>
        private void sql(int index, string str)
        {
            //参数变量
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@StuName",SqlDbType.VarChar),
                new SqlParameter("@StuAge",SqlDbType.Int),
                new SqlParameter("@StuSex",SqlDbType.Bit),
                new SqlParameter("@StuBirthday",SqlDbType.DateTime)

            };
          
            sqlParameters[0].Value = index;
            sqlParameters[1].Value = textBox3.Text;
            sqlParameters[2].Value = numericUpDown1.Value;
            sqlParameters[3].Value = comboBox1.Text == "男" ? 1 : 0;
            sqlParameters[4].Value = dateTimePicker1.Value;
           
            int num = -1;
            if (str == "修改")
            {

                num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), "UPDATE  Students  SET StuName = @StuName,StuAge=@StuAge,StuSex =@StuSex,StuBirthday =@StuBirthday WHERE  Id = @Id;", sqlParameters);
            }
            else if (str == "添加")
            {
                num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), "INSERT INTO Students(StuName,StuAge,StuSex,StuBirthday) VALUES(@StuName,@StuAge,@StuSex,@StuBirthday);", sqlParameters);
                MessageBox.Show("添加成功");
            }

            textBox2.Text = num.ToString();



        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void Add_modify_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
