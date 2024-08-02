using HRMS.Data;
using HRMS.Models;

namespace HRMS.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrmsdbContext _context;
        public DepartmentRepository(HrmsdbContext context)
        {
            _context = context;
        }
        public Department AddDepartment(Department department)
        {
            string spclChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            var nums = "1234567890";
            var deptCheck = _context.Departments.FirstOrDefault(x => x.DepartmentName == department.DepartmentName);

            if (deptCheck == null)
            {
                if (!(department.DepartmentName.Any(c => spclChar.Contains(c)) || department.DepartmentName.Any(c => nums.Contains(c))))
                {
                    if (!(department.Location.Any(c => spclChar.Contains(c))))
                    {
                        _context.Add(department);
                        _context.SaveChanges();
                        return department;
                    }
                    else
                    {
                        throw new CustomException("Department location should not contain numbers or special characters.");
                    }
                }
                else
                {
                    throw new CustomException("Department name should not contain numbers or special characters.");
                }
            }
            else
            {
                throw new CustomException("Department name already exists.");
            }

        }

        public List<Department> GetAllDepartment()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("Department List is empty");
            }
        }

        public List<Department> GetDepartment(int? deptId, string? deptName, string? location)
        {
            try
            {
                if (deptId != null && deptId > 0)
                {
                    List<Department> dept = _context.Departments.Where(x => x.DepartmentId == deptId).ToList();
                    if (dept.Count > 0)
                    {
                        return dept;
                    }
                    else
                    {
                        throw new CustomException("Department list is empty");
                    }
                }
                else if (!string.IsNullOrEmpty(deptName))
                {
                    return _context.Departments.Where(x => x.DepartmentName.Contains(deptName)).ToList();
                }
                else if (location != null)
                {
                    return _context.Departments.Where(y => y.Location.Contains(location)).ToList();
                }
                else
                {
                    throw new CustomException("Input field is empty");
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("Unable to get department details");
            }
        }

        public Department UpdateDepartment(UpdateDepartment department, int deptId)
        {
            try
            {
                var existingDepartment = _context.Departments.FirstOrDefault(x => x.DepartmentId == deptId);
                if (existingDepartment != null)
                {
                    existingDepartment.Location = department.Location;
                    existingDepartment.DepartmentName = department.DepartmentName;
                    _context.SaveChanges();
                    return existingDepartment;
                }
                else
                {
                    throw new CustomException("There is no record matching with department Id you gave");
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("Error occured");
            }
        }
    }
}
