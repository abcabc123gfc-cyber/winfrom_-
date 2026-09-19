using PlcComm.Slmp;
using System.Threading.Tasks;

namespace 三菱SLMP_
{
    public partial class Form1 : Form
    {
        SlmpConnectionOptions slmpConnectionOptions;
        SlmpClient slmpClient;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            slmpConnectionOptions = new SlmpConnectionOptions("192.168.1.1", SlmpPlcProfile.IqF, 2001, SlmpTransportMode.Tcp, SlmpTargetAddress.OwnStation);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            slmpClient = await SlmpClientFactory.OpenAndConnectAsync(slmpConnectionOptions);
            if (!slmpClient.IsOpen)
            {
                slmpClient.Open();

            }
            else
            {
                MessageBox.Show("已开启");
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {


            if (slmpClient == null) return;
            //BIT : 在 slmp库中 是专门用于 位设备 的数据类型
            //例如 x y m l 等 明确代表一位(bit) 内存单元
            var value = await slmpClient.ReadNamedAsync(["X0:BIT"]);  // 读一个字

            var v1 = value["X0:BIT"];

            MessageBox.Show(v1.ToString());
        }



        private async void button3_Click(object sender, EventArgs e)
        {
            if (slmpClient == null)
            {
                return;
            }
            await slmpClient.WriteTypedAsync("M0", "BIT", true);
            var v = await slmpClient.ReadNamedAsync(["M0:BIT"]);
            MessageBox.Show(v.ToString());
        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            if (slmpClient == null)
            {
                return;
            }
            await slmpClient.WriteTypedAsync("M0", "BIT", false);
            var v = await slmpClient.ReadNamedAsync(["M0:BIT"]);
            MessageBox.Show(v.ToString());
        }
    }
}
