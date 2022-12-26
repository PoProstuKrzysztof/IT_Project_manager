namespace IT_Project_manager.Models;

public class Team_Member
{

    public int MemberId { get; set; }
    public Team Team { get; set; }



    public int TeamId { get; set; }
    public Member Member { get; set; }

}
