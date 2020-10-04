using System.Collections.Generic;
namespace web_api
{
    public static class StudentRepo
    {
        public static List<Student> Studenti=new List<Student>() {
            new Student() {ID=1, Nume="Blagovici", Prenume="Cassandra", Facultatea="AC", An=3},
            new Student() {ID=2, Nume="Touluse", Prenume="Andreas", Facultatea="MPT", An=1}
        };
    }
}