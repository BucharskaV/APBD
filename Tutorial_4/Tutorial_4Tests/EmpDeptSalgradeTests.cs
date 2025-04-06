﻿using Tutorial_4;
using Tutorial_4.Models;

namespace Tutorial_4Tests;

public class EmpDeptSalgradeTests
{
    // 1. Simple WHERE filter
    // SQL: SELECT * FROM Emp WHERE Job = 'SALESMAN';
    [Fact]
    public void ShouldReturnAllSalesmen()
    {
        var emps = Database.GetEmps();

        List<Emp> result = emps
            .Where(emp => emp.Job == "SALESMAN")
            .ToList(); 

        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.Equal("SALESMAN", e.Job));
    }

    // 2. WHERE + OrderBy
    // SQL: SELECT * FROM Emp WHERE DeptNo = 30 ORDER BY Sal DESC;
    [Fact]
    public void ShouldReturnDept30OrderedBySalaryDesc()
    {
        var emps = Database.GetEmps();

        List<Emp> result =  emps
            .Where(emp => emp.DeptNo == 30)
            .OrderByDescending(emp => emp.Sal)
            .ToList(); 

        Assert.Equal(2, result.Count);
        Assert.True(result[0].Sal >= result[1].Sal);
    }

    // 3. Subquery using LINQ (IN clause)
    // SQL: SELECT * FROM Emp WHERE DeptNo IN (SELECT DeptNo FROM Dept WHERE Loc = 'CHICAGO');
    [Fact]
    public void ShouldReturnEmployeesFromChicago()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        List<Emp> result = emps
            .Where(emp => depts
                .Where(dept => dept.Loc == "CHICAGO")
                .Select(d => d.DeptNo)
                .Contains(emp.DeptNo))
            .ToList(); 

        Assert.All(result, e => Assert.Equal(30, e.DeptNo));
    }

    // 4. SELECT projection
    // SQL: SELECT EName, Sal FROM Emp;
    [Fact]
    public void ShouldSelectNamesAndSalaries()
    {
        var emps = Database.GetEmps();

        var result = emps
            .Select(emp => (emp.EName, emp.Sal))
            .ToList(); 
        
        Assert.All(result, r =>
        {
            Assert.False(string.IsNullOrWhiteSpace(r.EName));
            Assert.True(r.Sal > 0);
        });
    }

    // 5. JOIN Emp to Dept
    // SQL: SELECT E.EName, D.DName FROM Emp E JOIN Dept D ON E.DeptNo = D.DeptNo;
    [Fact]
    public void ShouldJoinEmployeesWithDepartments()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();

        var result = emps
            .Join(depts,
                emp => emp.DeptNo,
                dept => dept.DeptNo,
                (emp, dept) => new { emp.EName, dept.DName })
            .ToList(); 

        Assert.Contains(result, r => r.DName == "SALES" && r.EName == "ALLEN");
    }

    // 6. Group by DeptNo
    // SQL: SELECT DeptNo, COUNT(*) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCountEmployeesPerDepartment()
    {
        var emps = Database.GetEmps();

        var result = emps
            .GroupBy(emp => emp.DeptNo)
            .Select(deptGroup =>new { DeptNo = deptGroup.Key, Count = deptGroup.Count() }); 
        
        Assert.Contains(result, g => g.DeptNo == 30 && g.Count == 2);
    }

    // 7. SelectMany (simulate flattening)
    // SQL: SELECT EName, Comm FROM Emp WHERE Comm IS NOT NULL;
    [Fact]
    public void ShouldReturnEmployeesWithCommission()
    {
        var emps = Database.GetEmps();

        var result = emps
            .Where(e => e.Comm != null)
            .Select(e => new {e.EName, e.Comm})
            .ToList();
        
        Assert.All(result, r => Assert.NotNull(r.Comm));
    }

    // 8. Join with Salgrade
    // SQL: SELECT E.EName, S.Grade FROM Emp E JOIN Salgrade S ON E.Sal BETWEEN S.Losal AND S.Hisal;
    [Fact]
    public void ShouldMatchEmployeeToSalaryGrade()
    {
        var emps = Database.GetEmps();
        var grades = Database.GetSalgrades();

        var result = emps
            .SelectMany(emp => 
                grades.Where(sal => emp.Sal >= sal.Losal && emp.Sal <= sal.Hisal), 
                (emp, sal) => new { emp.EName, sal.Grade })
            .ToList();
        
        Assert.Contains(result, r => r.EName == "ALLEN" && r.Grade == 3);
    }

    // 9. Aggregation (AVG)
    // SQL: SELECT DeptNo, AVG(Sal) FROM Emp GROUP BY DeptNo;
    [Fact]
    public void ShouldCalculateAverageSalaryPerDept()
    {
        var emps = Database.GetEmps();

        var result = emps
            .GroupBy(emp => emp.DeptNo)
            .Select(group => new 
                {DeptNo = group.Key, AvgSal = group.Average(emp => emp.Sal)})
            .ToList(); 
        
        Assert.Contains(result, r => r.DeptNo == 30 && r.AvgSal > 1000);
    }

    // 10. Complex filter with subquery and join
    // SQL: SELECT E.EName FROM Emp E WHERE E.Sal > (SELECT AVG(Sal) FROM Emp WHERE DeptNo = E.DeptNo);
    [Fact]
    public void ShouldReturnEmployeesEarningMoreThanDeptAverage()
    {
        var emps = Database.GetEmps();

        var result = emps
            .GroupBy(emp => emp.DeptNo)
            .Select(group => new
            {
                DeptNo = group.Key, AvgSal = group.Average(emp => emp.Sal)
            })
            .Join(emps,
                avg  => avg .DeptNo,
                emp => emp.DeptNo,
                (avg , emp) => new
                {
                    emp.EName,
                    emp.Sal,
                    avg.AvgSal
                })
            .Where(res => res.Sal > res.AvgSal)
            .Select(res => res.EName)
            .ToList();
        
        Assert.Contains("ALLEN", result);
    }
}