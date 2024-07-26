using Store_Management.Data.Models;
using Store_Management.Services;
using Store_Management.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Store_Management.ViewModels.EmployeeVM
{
    public class EmployeeDetailsVM : ViewModelBase
    {
        private EmployeeService EmployeeService { get; set; }
        public Employee.Role AccessLevel { get; set; }

        public RelayCommand SelectProfileImageCommand { get; set; }

        // Role
        public Array RoleList { get; } = RoleUtil.ToArray();

        public bool IsRoleEditable { get; set; }
        public bool IsActiveEditable { get; set; }
        public string RoleDisplay { get; set; }

        private string _statusDisplay;

        // Input fields
        private int? _employeeId;
        private string? _fullName;
        private string? _email;
        private Employee.Role _role;  // Declare _role field
        private int _roleId;
        private string? _citizenId;
        private int? _age;
        private string? _address;
        private string? _profileImage;
        private bool _isActive;
        private string _phoneNumber;
    

        // Validation error properties
        private string? _fullNameError;
        private string? _emailError;
        private string? _citizenIdError;
        private string? _ageError;
        private string? _addressError;
        private string? _profileImageError;

        #region Properties
        public int? EmployeeId { get => _employeeId; set => SetProperty(ref _employeeId, value); }
        public Employee.Role SelectedRole { get => _role; set => SetProperty(ref _role, value); }  // Property for _role
        public string StatusDisplay { get => _statusDisplay; set => SetProperty(ref _statusDisplay, value); }
        public string PhoneNumber { get => _phoneNumber; set => SetProperty(ref _phoneNumber, value); }
        public string FullName { get => _fullName; set { SetProperty(ref _fullName, value); ValidateFullName(); } }
        public string Email { get => _email; set { SetProperty(ref _email, value); ValidateEmail(); } }
        public int RoleId { get => _roleId; set => SetProperty(ref _roleId, value); }  // Property for _roleId
        public string? CitizenId { get => _citizenId; set { SetProperty(ref _citizenId, value); ValidateCitizenId(); } }
        public int? Age { get => _age; set { SetProperty(ref _age, value); ValidateAge(); } }
        public string? Address { get => _address; set { SetProperty(ref _address, value); ValidateAddress(); } }
        public string? ProfileImage { get => _profileImage; set { SetProperty(ref _profileImage, value); ValidateProfileImage(); } }
        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
        public string? ImageUrl { get => _profileImage; set => SetProperty(ref _profileImage, value); }
        public string? FullNameError { get => _fullNameError; set => SetProperty(ref _fullNameError, value); }
        public string? EmailError { get => _emailError; set => SetProperty(ref _emailError, value); }
        public string? CitizenIdError { get => _citizenIdError; set => SetProperty(ref _citizenIdError, value); }
        public string? AgeError { get => _ageError; set => SetProperty(ref _ageError, value); }
        public string? AddressError { get => _addressError; set => SetProperty(ref _addressError, value); }
        public string? ProfileImageError { get => _profileImageError; set => SetProperty(ref _profileImageError, value); }

        #endregion

        public EmployeeDetailsVM(int employeeId)
        {
            EmployeeService = new EmployeeService();

            AccessLevel = (Employee.Role)StoreSession.Instance.ActiveEmployee.RoleId;
            IsActiveEditable = IsRoleEditable = (AccessLevel == Employee.Role.ADMIN);

            UpdateEmployeeCommand = new(async obj => await UpdateEmployee());
            SelectProfileImageCommand = new RelayCommand(_ => SelectProfileImage());

            LoadEmployeeData(employeeId);
        }

        public async Task LoadEmployeeData(int employeeId)
        {
            Employee? emp = await EmployeeService.FindById(employeeId);
            if (StoreSession.Instance.ActiveEmployee.Id == emp.Id)
            {
                StoreSession.Instance.ActiveEmployee = emp;
            }

            if (emp == null)
            {
                Notification.Error("Employee not found!");
                return;
            }

            this.EmployeeId = emp.Id;
            this.FullName = emp.FullName;
            this.Email = emp.Email;
            this.RoleId = emp.RoleId;  // Set RoleId from emp
            this.CitizenId = emp.CitizenId;
            this.Age = emp.Age ?? null;
            this.Address = emp.Address;
            this.ProfileImage = emp.ProfileImage; // Ensure ProfileImage is loaded correctly
            this.IsActive = emp.IsActive;
            this.PhoneNumber = emp.PhoneNumber;
            this.SelectedRole = (Employee.Role)emp.RoleId;
            this.StatusDisplay = IsActive ? "Active" : "Inactive";
            RoleDisplay = RoleUtil.ToRoleName(this.RoleId);
        }


        public async Task UpdateEmployee()
        {
            ValidateFullName();
            ValidateEmail();
            ValidateCitizenId();
            ValidateAge();
            ValidateAddress();
            ValidateProfileImage();

            if (string.IsNullOrEmpty(FullNameError) &&
                string.IsNullOrEmpty(EmailError) &&
                string.IsNullOrEmpty(CitizenIdError) &&
                string.IsNullOrEmpty(AgeError) &&
                string.IsNullOrEmpty(AddressError) &&
                string.IsNullOrEmpty(ProfileImageError))
            {
                try
                {
                    // Handle image upload
                    string imagePath = ProfileImage; // Default to existing path string imageDirectory = "path/to/your/image/directory"; 
                    if (!string.IsNullOrEmpty(ProfileImage) && File.Exists(ProfileImage))
                    {
                        string imageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeeImages");

                        var destPath = Path.Combine(imageDirectory, Path.GetFileName(ProfileImage));

                        // Create directory if it doesn't exist
                        if (!Directory.Exists(imageDirectory))
                        {
                            Directory.CreateDirectory(imageDirectory);
                        }

                        File.Copy(ProfileImage, destPath, true);
                        imagePath = destPath;
                    }

                    Employee emp = new Employee
                    {
                        Id = EmployeeId.Value,
                        Address = Address,
                        ProfileImage = imagePath, 
                        PhoneNumber = PhoneNumber,
                        Age = Age,
                        CitizenId = CitizenId,
                        Email = Email,
                        RoleId = (int)SelectedRole,
                        FullName = FullName,
                        IsActive = IsActive,
                        UpdatedBy = StoreSession.Instance.ActiveEmployee.Id,
                    };
                    await EmployeeService.Update(emp);
                    await LoadEmployeeData(this.EmployeeId.Value);

                    Notification.Success("Update details success!");
                }
                catch (Exception ex)
                {
                    Notification.Error(ex.ToString());
                }
            }
            else
            {
                Notification.Error("Validation failed. Please check the input values.");
            }
        }

        private void SelectProfileImage()
        {
            string path = DialogUtil.OpenImagePicker();
            ProfileImage = path;
        }

        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "Full Name is required.";
            }
            else
            {
                FullNameError = null;
            }
        }

        private void ValidateEmail()
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(Email);
                EmailError = null;
            }
            catch
            {
                EmailError = "Invalid email format.";
            }
        }

        private void ValidateCitizenId()
        {
            if (string.IsNullOrWhiteSpace(CitizenId))
            {
                CitizenIdError = "Citizen ID is required.";
            }
            else
            {
                CitizenIdError = null;
            }
        }

        private void ValidateAge()
        {
            if (Age.HasValue && Age.Value < 0)
            {
                AgeError = "Age cannot be negative.";
            }
            else
            {
                AgeError = null;
            }
        }

        private void ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(Address))
            {
                AddressError = "Address is required.";
            }
            else
            {
                AddressError = null;
            }
        }

        private void ValidateProfileImage()
        {
            if (!string.IsNullOrWhiteSpace(ProfileImage) && !File.Exists(ProfileImage))
            {
                ProfileImageError = "Profile Image file does not exist.";
            }
            else
            {
                ProfileImageError = null;
            }
        }
        public RelayCommand UpdateEmployeeCommand { get; set; }
    }
}
