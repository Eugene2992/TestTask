using System;

public enum Positions
{
    �����������,
    �����������,
    ������_��������,
    ��������
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
