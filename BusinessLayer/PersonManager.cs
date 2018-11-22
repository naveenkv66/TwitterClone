using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PersonManager
    {
        public string Save(Person person)
        {
            using (var db = new MVCApplicationEntities())
            {
                var upPerson = db.People.Where(a => a.User_Id.ToLower().Equals(person.User_Id.ToLower()) || a.Email.ToLower().Equals(person.Email.ToLower())).FirstOrDefault();
                var rePerson = db.People.Where(a => a.User_Id.ToLower().Equals(person.User_Id.ToLower()) && a.Active == false && a.Email.ToLower().Equals(person.Email.ToLower())).FirstOrDefault();
                if (upPerson == null)
                {
                    db.People.Add(person);
                    db.SaveChanges();
                }
                else if (upPerson != null && rePerson != null)
                {
                    rePerson.Password = person.Password;
                    rePerson.FullName = person.FullName;
                    rePerson.Email = person.Email;
                    rePerson.Active = true;
                    db.SaveChanges();
                }
                else
                {
                    return "UserName or EmailId already registered with us";
                }

                return "success";
            }
        }

        public string Update(Person person)
        {
            using (var db = new MVCApplicationEntities())
            {
                var upPerson = db.People.Where(a => a.User_Id.ToLower().Equals(person.User_Id.ToLower())).FirstOrDefault();
                var updatePerson = db.People.Where(a => !a.User_Id.ToLower().Equals(person.User_Id.ToLower()) && a.Email.ToLower().Equals(person.Email.ToLower())).FirstOrDefault();
                if (upPerson != null && updatePerson == null)
                {
                    upPerson.Password = person.Password ?? upPerson.Password;
                    upPerson.FullName = person.FullName;
                    upPerson.Email = person.Email;
                    upPerson.Active = person.Active;
                    db.SaveChanges();
                }
                else
                {
                    return "EmailId already registered with other user";
                }

                return "success";
            }
        }

        public Person Login(string userName, string password)
        {
            using (var db = new MVCApplicationEntities())
            {
                var person = db.People.Where(a => a.User_Id.ToLower().Equals(userName.ToLower()) && a.Password.Equals(password) && a.Active == true).FirstOrDefault();
                return person;
            }
        }

        public void SaveTweet(Tweet tweet)
        {
            using (var db = new MVCApplicationEntities())
            {
                db.Tweets.Add(tweet);
                db.SaveChanges();
            }
        }

        public Collection<Tweet> GetTweets(string userId)
        {
            Collection<Tweet> tweets = new Collection<Tweet>();
            using (var db = new MVCApplicationEntities())
            {
                db.Tweets.Where(x => x.User_Id == userId).ToList()
                    .ForEach(y =>
                    tweets.Add(new Tweet()
                    {
                        Tweet_Id = y.Tweet_Id,
                        User_Id = y.User_Id,
                        Message = y.Message,
                        Created = y.Created

                    }));
            }

            return tweets;
        }

        public Collection<Tweet> GetFollowingTweets(string userId)
        {
            Collection<Tweet> tweets = new Collection<Tweet>();
            using (var db = new MVCApplicationEntities())
            {
                db.Tweets.Join(db.Followings, r => r.User_Id, p => p.User_Id, (r, p) =>
                       new
                       {
                           r.User_Id,
                           r.Tweet_Id,
                           p.Following_Id,
                           r.Message,
                           r.Created
                       }).Where(x=>x.User_Id!=userId).ToList()
                    .ForEach(y =>
                    tweets.Add(new Tweet()
                    {
                        Tweet_Id = y.Tweet_Id,
                        User_Id = y.Following_Id,
                        Message = y.Message,
                        Created = y.Created

                    }));
            }

            return tweets;
        }

        public Collection<Following> GetFollowings(string userId)
        {
            Collection<Following> follwings = new Collection<Following>();
            using (var db = new MVCApplicationEntities())
            {
                db.Followings.Where(x => x.User_Id == userId)
                    .ToList()
                    .ForEach(y =>
                    follwings.Add(new Following()
                    {
                        User_Id = y.User_Id,
                        Following_Id = y.Following_Id
                    }));
            }

            return follwings;
        }
      

        public List<string> SearchUsersByName(string name)
        {
            using (var db = new MVCApplicationEntities())
            {
                List<string> names = db.People.Where(item => item.User_Id.StartsWith(name) || item.User_Id.Contains(name)).Select(item => item.User_Id).ToList();
                return names;
            }
        }

        public Collection<Following> GetFollowers(string userId)
        {
            Collection<Following> followers = new Collection<Following>();
            using (var db = new MVCApplicationEntities())
            {
                db.Followings.Where(x => x.Following_Id == userId)
                    .ToList()
                    .ForEach(y =>
                    followers.Add(new Following()
                    {
                        User_Id = y.User_Id,
                        Following_Id = y.Following_Id
                    }));
            }

            return followers;
        }

        public void FollowUser(string userId, string follwerId)
        {
            using (var db = new MVCApplicationEntities())
            {
                db.Followings.Add(new Following { User_Id = userId, Following_Id = follwerId });
                db.SaveChanges();
            }
        }
    }
}
