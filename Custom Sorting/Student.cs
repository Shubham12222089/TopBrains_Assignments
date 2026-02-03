class Student
{
    public string Name="";
    public int Age;
    public int Marks;

}
class StudentComparer: IComparer<Student>
{
    public int Compare(Student? s1,Student? s2)
    {
        // Negative → first argument pehle
        // Positive → second argument pehle
        // Zero → dono same position
        
         // Agar dono null hain → equal
        if (s1 == null && s2 == null) return 0;

        // Agar s1 null hai → s2 pehle aaye
        if (s1 == null) return 1;

        // Agar s2 null hai → s1 pehle aaye
        if (s2 == null) return -1;

        if(s1.Marks != s2.Marks)
        {
            return s2.Marks.CompareTo(s1.Marks); // high to low check
        }
        return s1.Age.CompareTo(s2.Age); // low to high check
    }
}