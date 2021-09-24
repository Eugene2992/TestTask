using System;

public enum Positions
{
    Разработчик,
    Тестировщик,
    Бизнес_аналитик,
    Менеджер
}
public class Worker
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Patronymic { get; set; }
    public DateTime DateRecruitment { get; set; }
    public Positions Position { get; set; }
    public string Company { get; set; }
}
