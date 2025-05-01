namespace FreelancePlatformEFTask;

public class Bullshittery
{
    public string Logo = $"            _    _   __          __        _    \n" +
                         $"           | |  | |  \\ \\        / /       | |   \n" +
                         $"  ___  ___ | |  | |_ _\\ \\  /\\  / /__  _ __| | __\n" +
                         $" / __|/ _ \\| |  | | '_ \\ \\/  \\/ / _ \\| '__| |/ /\n" +
                         $" \\__ \\ (_) | |__| | |_) \\  /\\  / (_) | |  |   < \n" +
                         $" |___/\\___/ \\____/| .__/ \\/  \\/ \\___/|_|  |_|\\_\\ \n" +
                         $"                  | |                           \n" +
                         $" spaghetti-code?  |_|  we're doing soup-code here!\n";

    public string HelpM = "* Main commands:\n" +
                          "* help - shows this window.\n" +
                          "* exit - exits the program.\n" +
                          "* me - shows your account info.\n" +
                          "* finances - access finance menu.\n" +
                          "* projects - browse projects.\n" +
                          "* chat - access chat menu.\n" +
                          "* reviews (user Id) - browse user's reviews." +
                          "* rmreview (review Id) - remove review.\n";

    public string HelpF = "* Role specific commands:\n" +
                          "* myprojects - browse projects you're working on.\n" +
                          "* apply (project Id) (asking price) - apply to a pending project.\n" +
                          "* review (user Id) - leave a review on client.\n" +
                          "* rmreview (review Id) - remove review.\n";

    public string HelpC = "* Role specific commands:\n" +
                          "* myprojects - browse projects you've created.\n" +
                          "* newproject - create a new project.\n" +
                          "* rmproject (project Id) - remove project." +
                          "* applications - browse applications for your projects.\n" +
                          "* approve (application Id) - approve application.\n" +
                          "* deny (application Id) - deny application.\n" +
                          "* review (user Id) - leave a review of freelancer.\n";

    public string HelpA = "* Debugging commands:\n" +
                          "* printmoney - add $10,000.00 to currentUser balance.\n";
}