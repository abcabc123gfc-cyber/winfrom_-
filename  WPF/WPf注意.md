在使用字典绑定 ComBox

~~~
 SelectedIndex="0" 值的设定是根据  
 SelectedValue="{Binding TemperatureStatus}"
 当 TemperatureStatus 值真实与地点的 value 对应时才可以生效,类型也要一致
~~~

~~~c#
 <ComboBox
     x:Name="cboTemperatureStatus"
     Grid.Column="1"
     DisplayMemberPath="Key"
     ItemsSource="{Binding dicTemperatureStatus}"
     SelectedIndex="0"
     SelectedValue="{Binding TemperatureStatus}"
     SelectedValuePath="Value" />
~~~

~~~C#
 //绑定值
 /// <summary>
 /// 温度状态
 /// </summary>
 private int temperatureStatus;
 public int TemperatureStatus
 {
     get
     {
         return temperatureStatus;
     }
     set
     {
         temperatureStatus = value;
         OnPropertyChanged();
     }
 }
 //字典
  public Dictionary<string, int> dicTemperatureStatus { get; set; } = new Dictionary<string, int>() { { "正常", 0 }, { "高预警", 1 }, { "低预警", 2 } };
~~~

