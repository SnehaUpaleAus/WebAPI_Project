using System;

namespace WebAPI_Project
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }

        public string name { get; set; }
        public string company { get; set; }
        public long followers { get; set; }

        public long public_repos { get; set; }

        public decimal avg_Followers_Per_Repo
        {
            get
            {
                if (public_repos > 0)
                {
                                       
                    return Math.Round(Decimal.Divide(followers, public_repos), 2);

                }
                else
                {
                    return 0;
                }
            }
        }





    }
}

