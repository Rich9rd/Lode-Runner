using System;

namespace LRCN
{

    public class User
    {
        public int Score { set; get; }
        public string Name { set; get; }

        public User()
        {
            Name = "----";
            Score = 0;
        }

        public User(int score, string name)
        {
            Score = score;
            Name = name;
        }
    }

    public class TableOfRecords
    {
        public bool IsNewRecord { set; get; }
        public User[] Table = new User[5];

        public TableOfRecords()
        {
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = new User();
            }
        }
        
        public void Add(User user)
        {            
            if (user.Score > Table[0].Score)
            {
                if (user.Score > Table[Table.Length-1].Score)
                    IsNewRecord = true;
                Table[0] = user;
                SortTable(Table);
            }
            else
            {
                IsNewRecord = false;
            }

            void SortTable(User[] users)
            {
                User temp;
                for (int write = 0; write < users.Length; write++)
                {
                    for (int sort = 0; sort < users.Length - 1; sort++)
                    {
                        if (users[sort].Score > users[sort + 1].Score)
                        {
                            temp = users[sort + 1];
                            users[sort + 1] = users[sort];
                            users[sort] = temp;
                        }
                    }
                }
            }
        }
    }
}