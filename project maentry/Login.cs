using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using maentry;
using Npgsql;

namespace project_maentry
{
    public partial class Login : Form
    {
        // Encapsulation
        private readonly IAuthenticationService _authService;

        public Login()
        {
            InitializeComponent();
            _authService = new AuthenticationService(new SiapaUser());
        }

        // Polymorphism
        public Login(IAuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await HandleLoginAsync();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            NavigateToRegister();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Encapsulation
        private async Task HandleLoginAsync()
        {
            try
            {
                SetButtonState(false);

                string username = GetUsernameInput();
                string password = GetPasswordInput();

                AuthResult result = await _authService.AuthenticateAsync(username, password);

                if (result.IsSuccess)
                {
                    HandleSuccessfulLogin(result.User);
                }
                else
                {
                    HandleFailedLogin(result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Terjadi kesalahan sistem: {ex.Message}");
            }
            finally
            {
                SetButtonState(true);
            }
        }

        private string GetUsernameInput()
        {
            return txtusername.Text.Trim();
        }

        private string GetPasswordInput()
        {
            return txtpassword.Text.Trim();
        }

        private void SetButtonState(bool enabled)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button &&
                    (button.Name == "button1" || button.Name.ToLower().Contains("login")))
                {
                    button.Enabled = enabled;
                    break;
                }
            }
        }

        private void HandleSuccessfulLogin(User user)
        {
            ShowSuccessMessage(user.Role);

            // Polymorphism
            user.NavigateToMainUI();

            HideCurrentForm();
        }

        private void HandleFailedLogin(string message)
        {
            ShowErrorMessage(message);
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtusername.Focus();
        }

        private void HideCurrentForm()
        {
            this.Hide();
        }

        private void ExitApplication()
        {
            Application.Exit();
        }

        private void NavigateToRegister()
        {
            Register registerform = new Register();
            registerform.Show();
            this.Hide();
        }

        private void ShowSuccessMessage(string role)
        {
            MessageBox.Show($"Login berhasil sebagai {role}", "Informasi",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Abstraction
    public abstract class User
    {
        // Encapsulation
        public int UserId { get; protected set; }
        public string Username { get; protected set; }
        public string Role { get; protected set; }

        protected User(int userId, string username, string role)
        {
            UserId = userId;
            Username = username;
            Role = role;
        }

        // Abstraction
        public abstract void NavigateToMainUI();
    }

    // Inheritance
    public class Mahasiswa : User
    {
        // Encapsulation
        public string Nim { get; private set; }

        public Mahasiswa(int userId, string username, string nim)
            : base(userId, username, "mahasiswa")
        {
            Nim = nim;
        }

        // Polymorphism
        public override void NavigateToMainUI()
        {
            new mahasiswaUI(Nim).Show();
        }
    }

    // Inheritance
    public class Dosen : User
    {
        // Encapsulation
        public string Nip { get; private set; }

        public Dosen(int userId, string username, string nip)
            : base(userId, username, "dosen")
        {
            Nip = nip;
        }

        // Polymorphism
        public override void NavigateToMainUI()
        {
            new dosenUI().Show();
        }
    }

    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public User User { get; set; }

        public static AuthResult Success(User user)
        {
            return new AuthResult { IsSuccess = true, User = user };
        }

        public static AuthResult Failure(string message)
        {
            return new AuthResult { IsSuccess = false, Message = message };
        }
    }

    // Abstraction
    public interface IAuthenticationService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
    }

    // Abstraction
    public interface ISiapaUser
    {
        User CreateUser(string role, int userId, string username, string nim, string nip);
    }

    // Encapsulation
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISiapaUser _siapaUser;

        public AuthenticationService(ISiapaUser siapaUser)
        {
            _siapaUser = siapaUser;
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            try
            {
                if (!IsValidInput(username, password))
                {
                    return AuthResult.Failure("Username dan password tidak boleh kosong.");
                }

                using (var conn = Database.GetConnection())
                {
                    await conn.OpenAsync();

                    string query = @"
                        SELECT u.role, u.user_id, m.nim, d.nip 
                        FROM users u
                        LEFT JOIN mahasiswa m ON u.user_id = m.user_id
                        LEFT JOIN dosen d ON u.user_id = d.user_id
                        WHERE u.username = @username AND u.password = md5(@password)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string role = reader["role"].ToString();
                                int userId = Convert.ToInt32(reader["user_id"]);
                                string nim = reader["nim"]?.ToString() ?? "";
                                string nip = reader["nip"]?.ToString() ?? "";

                                // Polymorphism
                                User user = _siapaUser.CreateUser(role, userId, username, nim, nip);

                                if (user == null)
                                {
                                    return AuthResult.Failure("Data pengguna tidak lengkap.");
                                }

                                return AuthResult.Success(user);
                            }
                            else
                            {
                                return AuthResult.Failure("Username atau password salah, silakan coba lagi.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"Terjadi kesalahan: {ex.Message}");
            }
        }

        // Encapsulation
        private bool IsValidInput(string username, string password)
        {
            return !string.IsNullOrEmpty(username?.Trim()) && !string.IsNullOrEmpty(password?.Trim());
        }
    }

    public class SiapaUser : ISiapaUser
    {
        // Polymorphism
        public User CreateUser(string role, int userId, string username, string nim, string nip)
        {
            switch (role?.ToLower())
            {
                case "mahasiswa":
                    if (string.IsNullOrEmpty(nim))
                        return null;
                    return new Mahasiswa(userId, username, nim);

                case "dosen":
                    return new Dosen(userId, username, nip);

                default:
                    return null;
            }
        }
    }
}
