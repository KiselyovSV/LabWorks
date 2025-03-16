using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfAppAdo.net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ConnectionState? state;
        private static PasswordWindow? passWindow;
        private static AdditionWindow? addWindow;
        private static string connString = "";
        private NpgsqlConnection? conn;
        private Dictionary<string, int> opr = new Dictionary<string, int>();
        private int offset = 0;
        private int total = 0;
        private const int PAGE_SIZE = 15;
        private string? save = GetSetting("Save");
        private string? pred;
        private string? pr;

        public MainWindow()
        {
            InitializeComponent();
            if(save == "True")
            {
                string? confUsernameString = GetSetting("Username");
                string? confPasswordString = GetSetting("Password");
                this.btnLogin.Click -= btnLogin_Click;
                Authorization(confUsernameString, confPasswordString);
            }
                
        }

        public void loadPage()
        {
            try
            {
                conn?.Open();
                state = conn?.State;
                lstOrganicprod.Items.Clear();

                NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT count(*) FROM organic_prod_t AS op, organi_sertifikatsii_t AS os, imena_proizvoditeley_t AS ip,
                cert_states_t AS cs WHERE op.organ_sertifikatsii_id = os.organ_sertifikatsii_id AND op.cert_state_id = cs.cert_state_id
                AND op.proizvoditel_name_id = ip.proizvoditel_name_id {0};", pr), conn);

                total = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new NpgsqlCommand(string.Format(@"SELECT op.reg_number, op.date, os.organ_sertifikatsii_name, os.organ_sert_inn, ip.proizvoditel_name,
                COALESCE(ip.proizvoditel_inn, 0), COALESCE(ip.remark,'отсутствует'), op.cert_num, op.cert_date_sign, op.cert_expire, cs.cert_states_name
                FROM organic_prod_t AS op
                JOIN organi_sertifikatsii_t AS os ON op.organ_sertifikatsii_id = os.organ_sertifikatsii_id
                JOIN imena_proizvoditeley_t AS ip ON op.proizvoditel_name_id = ip.proizvoditel_name_id
                JOIN cert_states_t AS cs ON op.cert_state_id = cs.cert_state_id {2} LIMIT {0} OFFSET {1};", PAGE_SIZE, offset, pred), conn);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                string item;
                opr.Clear();
                myLabel.Content = "";
                int p = 0;
                if (total % PAGE_SIZE > 0) p = total / PAGE_SIZE + 1;
                else p = total / PAGE_SIZE;
                myLabel.Content = $"стр. {Math.Min(offset / PAGE_SIZE + 1, p)} из {Convert.ToString(p)}";
                while (reader.Read())
                {
                    DateTime date1 = reader.GetDateTime(1);
                    DateTime date8 = reader.GetDateTime(8);
                    DateTime date9 = reader.GetDateTime(9);
                    item = reader.GetInt32(0) + ", " + date1.Day + "." + date1.Month + "." + date1.Year + ", " + reader.GetString(2) + ", " + reader.GetInt64(3) +
                    ", " + reader.GetString(4) + ", " + reader.GetInt64(5) + ", " + reader.GetString(7) + ", " + date8.Day + "." + date8.Month + "." + date8.Year + 
                    ", " + date9.Day + "." + date9.Month + "." + date9.Year + ", " + reader.GetString(10);

                    lstOrganicprod.Items.Add(item);
                    if (!opr.ContainsKey(item))
                    {
                        opr.Add(item, reader.GetInt32(0));
                    }
                }
                reader.Close();
            }    
            catch (PostgresException e)
            {
                Debug.WriteLine("Failure(Неудача): " + e.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failure(Неудача): " + ex.Message);
                MessageBox.Show("Подключение не может быть установлено","Сообщение",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            { 
                if (conn?.State != System.Data.ConnectionState.Closed)
                {
                    conn?.Close();
                }
            }
        }
        private void lstOrganicprod_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                conn?.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT COALESCE(ip.remark,'отсутствует')
                FROM organic_prod_t AS op
                JOIN imena_proizvoditeley_t AS ip ON op.proizvoditel_name_id = ip.proizvoditel_name_id
                WHERE op.reg_number = {0};", opr[(string)lstOrganicprod.SelectedItem]), conn);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                string item;
                if (reader.Read())
                {
                    item = reader.GetString(0);
                    commentTextBox.Clear();
                    commentTextBox.Text = item;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failure(Неудача): " + ex.Message);
            }
            finally
            {
                if (conn?.State != System.Data.ConnectionState.Closed) conn?.Close();
            }
        }
        private void lstOrganicprod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                conn?.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT op.reg_number, op.date, os.organ_sertifikatsii_name, os.organ_sert_inn, ip.proizvoditel_name,
                COALESCE(ip.proizvoditel_inn, 0), COALESCE(ip.remark,'отсутствует'), op.cert_num, op.cert_date_sign, op.cert_expire, cs.cert_states_name
                FROM organic_prod_t AS op
                JOIN organi_sertifikatsii_t AS os ON op.organ_sertifikatsii_id = os.organ_sertifikatsii_id
                JOIN imena_proizvoditeley_t AS ip ON op.proizvoditel_name_id = ip.proizvoditel_name_id
                JOIN cert_states_t AS cs ON op.cert_state_id = cs.cert_state_id WHERE op.reg_number = {0};", opr[(string)lstOrganicprod.SelectedItem]), conn);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Регистрационный номер: " + reader.GetInt32(0) + "\n" +
                    "Наименование: " + reader.GetString(4) + "\n" +
                    "Статус сертификата: " + reader.GetString(10), "Краткая информация о производителе", MessageBoxButton.OK);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failure(Неудача): " + ex.Message);
            }
            finally
            {
                if (conn?.State != System.Data.ConnectionState.Closed) conn?.Close();
            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if(offset > 0)
            {
                offset = Math.Max(0, offset - PAGE_SIZE);
                loadPage();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(offset + PAGE_SIZE <= total)
            {
                offset += PAGE_SIZE;
                loadPage();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            conn?.Dispose();
        }
        private void mainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((state == ConnectionState.Open && passWindow != null && passWindow.SaveData)|| save == "True")
            {
                var result = MessageBox.Show("Сохранить данные учётной записи?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    SetSetting("Username", "");
                    SetSetting("Password", "");
                    SetSetting("Save", "False");
                }
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            passWindow = new PasswordWindow();
            passWindow.Owner = this;

            if (passWindow.ShowDialog() == true)
            {
                if (passWindow.SaveData == true)
                {
                    SetSetting("Username", passWindow.Login);
                    SetSetting("Password", passWindow.Password);
                    SetSetting("Save", "True");

                    string? confUsernameString = GetSetting("Username");
                    string? confPasswordString = GetSetting("Password");
                    Debug.WriteLine(confUsernameString);
                    Debug.WriteLine(confPasswordString);
                    Authorization(confUsernameString, confPasswordString);
                }
                else
                {
                    Authorization(passWindow.Login, passWindow.Password);
                }
               
            }

        }
        
        private static string? GetSetting(string key)
        {
          return ConfigurationManager.AppSettings[key];
        }

        private static void SetSetting(string? key,string? value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Full,true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Authorization(string? username, string? password)
        {
            connString = string.Format(@"Host=141.8.193.236;Port=5432;Database=f1102267_organicprod;
            Server Compatibility Mode=NoTypeLoading;Pooling=false;Username={0};Password={1}", username, password);
            conn = new NpgsqlConnection(connString);
            loadPage();
            if (conn != null && state == ConnectionState.Open && save != "True")
            {
                this.btnLogin.Click -= btnLogin_Click;
                MessageBox.Show("Авторизация пройдена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (save != "True") MessageBox.Show("Авторизация не пройдена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAddMaker_Click(object sender, RoutedEventArgs e)
        {
            addWindow = new AdditionWindow();
            addWindow.Owner = this;
            string? st = addWindow.StatusCert;
            if (addWindow.ShowDialog() == true)
            {
                if (addWindow.StatusCert == "Прекращен" || addWindow.StatusCert == "Приостановлен" || addWindow.StatusCert == "Действует")
                {
                    try
                    {
                        conn?.Open();

                        NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"SELECT * FROM fn_addline ('{0}','{1}',{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}');",
                        addWindow.RegDate ?? "0000-00-00", addWindow.CertificOrganizationName ?? "No name", addWindow.CertificOrganizationTIN ?? "0000000000",
                        addWindow.MakerName ?? "No name", addWindow.MakerNameTIN ?? "0000000000", "Нет комментария", addWindow.CertificNumber ?? "No number",
                        addWindow.StartDateOfCert ?? "0000-00-00", addWindow.CertExpirationDate ?? "0000-00-00", st), conn);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Добавлена новая запись о регистрации", "Сообщение", MessageBoxButton.OK);
                    }
                    catch (NpgsqlException ex)
                    {
                        Debug.WriteLine("Failure(Неудача): " + ex.Message);
                        MessageBox.Show("Введите данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Failure(Неудача): " + ex.Message);
                    }
                    finally
                    {
                        if (conn?.State != System.Data.ConnectionState.Closed) conn?.Close();
                    }
                }
                else if(addWindow.RegDate =="" || addWindow.CertificOrganizationName =="" || addWindow.CertificOrganizationTIN =="" ||
                addWindow.MakerName =="" || addWindow.MakerNameTIN =="" || addWindow.CertificNumber =="" || 
                addWindow.StartDateOfCert =="" || addWindow.CertExpirationDate =="" || st == null || st =="") MessageBox.Show("Введите все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else if(addWindow.RegDate == "ГГГГ-ММ-ДД" || addWindow.CertificOrganizationName == "Наименование" || addWindow.CertificOrganizationTIN == "Номер ИНН" ||
                addWindow.MakerName == "Наименование" || addWindow.MakerNameTIN == "Номер ИНН" || addWindow.CertificNumber == "Номер сертификата" ||
                addWindow.StartDateOfCert == "ГГГГ-ММ-ДД" || addWindow.CertExpirationDate == "ГГГГ-ММ-ДД" || st == "Статус") MessageBox.Show("Введите данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else MessageBox.Show("Введите данные правильно, с учетом регистра", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnUpdateComment_Click(object sender, RoutedEventArgs e)
        {
            if (lstOrganicprod.SelectedItem is not null)
            {
                try
                {
                    conn?.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(string.Format(@"UPDATE imena_proizvoditeley_t AS ip
                SET remark = '{0}'
                FROM public.organic_prod_t AS op
                WHERE op.proizvoditel_name_id = ip.proizvoditel_name_id AND
                op.reg_number = {1};", commentTextBox.Text, opr[(string)lstOrganicprod.SelectedItem]), conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Обновление завершено", "Сообщение", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failure(Неудача): " + ex.Message);
                }
                finally
                {
                    if (conn?.State != System.Data.ConnectionState.Closed) conn?.Close();
                }
            }
            else MessageBox.Show("Необходимо выбрать производителя из списка", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnShowStatus_Click(object sender, RoutedEventArgs e)
        {

            if (rdBtnValid.IsChecked == true) { pred = "WHERE cs.cert_states_name = 'Действует'"; pr = "AND cs.cert_states_name = 'Действует'"; }
            else if (rdBtnEnded.IsChecked == true) { pred = "WHERE cs.cert_states_name = 'Прекращен'"; pr = "AND cs.cert_states_name = 'Прекращен'"; }
            else if (rdBtnPaused.IsChecked == true) { pred = "WHERE cs.cert_states_name = 'Приостановлен'"; pr = "AND cs.cert_states_name = 'Приостановлен'"; }
            else if (rdBtnAll.IsChecked == true) { pred = "WHERE 1=1"; pr = "AND 1=1"; }
            loadPage();
        }
    }
}