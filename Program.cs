using System.Data.Common;

namespace _3pcs

void task1()
{
    Console.WriteLine("Задание1");
    string programmName = "Русский язык";
    using (AppDbContext db = new AppDbContext())
    {
        var ans = db.Programm_Enrollees
            .Where(pe => pe.Programm.name_programm == programmName)
            .Select(pe => pe.Enrollee);
        foreach (var a in ans)
        {
            Console.WriteLine($"{a.name_enrollee}");
        }
    }
}
void task2()
{
    Console.WriteLine("Задание 2:");
    string subjectName = "Физика";
    using (var db=new AppDbContext())
    {
        var progamsWithSubject = db.Programm_Subjects
            .Where(ps => ps.Subject.name_subject == subjectName)
            .Select(ps => ps.Programm)
            .Distinct()
            .ToList();
        foreach (var programm in programsWithSubject)
        {
            Console.WriteLine($"ID:{programm.ID},Имя{programm.name_program}");
        }
    }
}
void task3()
{
    Console.WriteLine("Задание3:");
    using(var db = new AppDbContext())
    {
        var egeSubjects = db.Subjects.ToList();
        foreach (var subject in egeSubjects)
        {
            var subjectStats = db.Enrollee_Subjects
                .Where(es => es.Subject.ID == subject.ID)
                .Select(es => es.result)
                .ToList();
            if (subjectStats.Any())
            {
                var minScore = subjectStats.Min();
                var maxScore = subjectStats.Max();
                var enrolleeCount = subjectStats.Count;
                Console.WriteLine($"Предмет:{subject.name_subject}");
                Console.WriteLine($"Минимальный балл:{minScore}");
                Console.WriteLine($"Максимальный балл:{maxScore}");
                Console.WriteLine($"Количество абитуриентов:{enrolleeCount}");
            }
        }
    }
}
void task4()
{
    Console.WriteLine("Задание 4:");
    int minScoreThreshold = 80;
    using (var db = new AppDbContext())
    {
        var programs = db.Progranns.ToList();
        foreach (var programm in programs) {
            var minScores = db.Programm_Subjects
                .Where(ps => ps.Programm.ID == program.ID)
                .Select(ps => ps.min_result)
                .ToList();
            if (minScores.All(score => score > minScoreThreshold))
            {
                Console.WriteLine($"Образовательная программа:{programm.name_programm}");
            }

        }
    }
}
task5()
{
    Console.WriteLine("Задание 5:");
    using(var dbContext= new AppDbContext())
    {
        var programWithMaxEnrollees = dbContext.Programms
            .Where(p => p.ID == dbContext.Programm_Enrollees
            .GroupBy(pe => pe.Programm.ID)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault())
         .FirstOrDefault();
        if (programWithMaxEnrollees != null)
        {
            Console.WriteLine($"Образовательная программа с наибольшим планом набора:");
            Console.WriteLine($"ID:{programWithMaxEnrollees.ID} Название:{programWithMaxEnrollees.name_programm},План:{programWithMaxEnrollees.plan}");

        }
        else
        {
            Console.WriteLine("Образовательные программы не найдены.")
        } 
                

    }
}
void task6()
{
    Console.WriteLine("Задание 6:");
    using(var db = new AppDbContext())
    {
        var ans = db.Enrollee_Achievements
            .GroupBy(f => f.Enrollee.ID)
            .Select(a => new
            {
                name = a.Where(f => f.Enrollee.ID == a.Key).Select(e => e.Enrollee.name_enrollee).First(),
                score = a.Where(f => f.Enrollee.ID == a.Key).Select(s => s.Acgievement.bonus).Sum()
            })
            .ToList();
        foreach(var a in ans)
        {
            Console.WriteLine($"Индивидуальные баллы абитуриента{a.name}:{a.score}");

        }
    }
}
void task7()
{
    Console.WriteLine("Задание 7:");
    using (var db=new AppDbContext())
    {
        var ans = db.Programm_Enrollees
            .GroupBy(f => f.Programm.ID)
            .Select(a => new
            {
                prog = a.Where(f => f.Programm.ID == a.Key).Select(p => Programm.name_programm).First(),
                k = (double)a.Select(f => f.Enrollee).Count() / a.Select(f => f.Programm.plan).First()
            });
        foreach (var a in ans)
        {
            Console.WriteLine($"Конкурс на программу \"{a.prog}\":{a.k}");
        }
    }
}
void task8()
{
    string s1 = "Математика";
    string s2 = "Английский язык";
    Console.WriteLine("Задание 8:");
    using (var db = new AppDbContext())
    {
        var ans = db.Programms
            .Join(db.Programm_Subjects.Where(ps => ps.Subject.name_cubject == s1),
            p => p.ID,
            ps => ps.Programm.ID,
            (p, ps) => new { ProgrammID = p.ID, Subject1 = ps })
            .Join(db.Programm_Subjects.Where(ps => ps.Subject.name_subject == s2),
            pps => pps.ProgrammID,
            ps => ps.Programm.ID,
            (pps, ps) => new { pps.Subject1.Program, Subject2 = ps })
            .Select(p => p.Programm)
            .Distinct()
            .ToList();
        if (ans.Any())
        {
            Console.WriteLine($"Образовательные программы,которые для поступления необходимы предметы {s1} и {s2}:");
            foreach (var p in ans)
            {
                Console.WriteLine($"ID: {p.ID}, Название:{p.name_programm}");
            }
        }
        else
        {
            Console.WriteLine($"Образовательные программы, на которые дял поступления необходимы предметы {s1} и {s2}, не найдены.");
        }
    }
}
void task9() {
    Console.WriteLine("Задание 9:");
    using (var db = new AppDbContext())
    {
        var ans = db.Enrollee_Subjects
            .Join(db.Programm_Subjects,
            es => es.Subject.ID,
            ps => ps.Subject.ID,
            (es, ps) => new
            {
                EnrolleeID = es.Enrollee.ID, eName = es.Enrollee.name_enrollee,
                ProgrammID = ps.Programm.ID, pName = ps.Programm.name_programm, Score = es.result
            })
            .GroupBy(es => new { es.EnrolleeI, es.ProgrammID })
            .Select(group => new
            {
                EnrolleeID = group.Key.EnrolleeID,
                ProgrammID = group.Key.Programm.ID,
                TotalScore = group.Sum(es => es.Score)
            })
            .ToList();
        foreach (var score in ans)
        {
            var enrolleeName = db.Enrollees.FirstOrDefault(e => e.ID == score.EnrolleeID)?.name_enrollee;
            var programmName = db.Programms.FirstOrDefault(p => p.ID == score.ProgrammID)?.name_programm;
            Console.WriteLine($"Абитуриент:{enrolleeName}, Программа:{programmName}, Общий балл: {score.TotalScore}");
        }
    }
}
void task10()
{
    string p = "Математика";
    using(var dbContext = new AppDbContext())
    {
        var ans = dbContext.Enrollees
            .Where(e=> dbContext.Programm.ID
            .GroupBy(ps => ps.Programm.ID)
            .All(group => group
            .Any(ps => dbContext.Enrollee_Subjects
            .Where(es => es.Enrollee.ID == es.ID && es.Subject.ID == ps.Subject.ID)
            .Sum(es => es.result) >= ps.min_result)))
            .ToList();
        Console.WriteLine("Абитуриенты, которые не могут быть зачислены на образовательную программу:");
        foreach (var enrollee in ans)
        {
            Console.WriteLine($"ID: {enrollee.ID}, Name: {enrollee.name_enrollee}");
        }
    }
}
task1();
task2();
task3();
task4();
task5();
task6();
task7();
task8();
task9();
task10();