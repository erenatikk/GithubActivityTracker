# GitHub Activity Tracker 📊

A simple command-line tool to fetch and display GitHub user's recent public activities.

## ✨ Features

- 📋 View last 10 public activities of any GitHub user
- 🎨 Colorful console output with emojis
- 📅 Formatted date and time display
- 🛡️ Error handling for invalid users or API issues
- 🚀 Fast and lightweight

## 🚀 Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/yourusername/GithubActivityTracker.git
cd GithubActivityTracker
```

2. **Build the project:**
```bash
dotnet build
```

3. **Run the application:**
```bash
dotnet run <github-username>
```

## 📖 Usage

```bash
# View activities for a specific user
dotnet run octocat

# Examples with real users
dotnet run torvalds
dotnet run gaearon
dotnet run kentcdodds
```

### Sample Output
```
Last Activities of octocat:

• 📝 Pushed to octocat/Hello-World
  📅 07-09-2025 14:30

• ⭐ Starred microsoft/vscode
  📅 06-09-2025 09:15

• 🔀 Created pull request in facebook/react
  📅 05-09-2025 16:45
```

## 🔧 Supported Activity Types

| Activity | Icon | Description |
|----------|------|-------------|
| **PushEvent** | 📝 | Code pushed to repository |
| **CreateEvent** | 🆕 | New repository/branch created |
| **WatchEvent** | ⭐ | Repository starred |
| **ForkEvent** | 🍴 | Repository forked |
| **IssuesEvent** | 🐛 | Issue opened/closed |
| **PullRequestEvent** | 🔀 | Pull request created |
| **FollowEvent** | 👥 | Started following someone |
| **DeleteEvent** | 🗑️ | Something deleted |

## 📁 Project Structure

```
GithubActivityTracker/
├── Program.cs                 # Main application logic
├── GithubActivityTracker.csproj   # Project configuration
└── README.md                  # Project documentation
```

## 🛠️ Technologies Used

- **C#** - Programming language
- **.NET 8.0** - Runtime framework  
- **System.Text.Json** - JSON parsing
- **GitHub REST API** - Data source

## 🔗 GitHub API

This tool uses the [GitHub Events API](https://docs.github.com/en/rest/activity/events#list-public-events-for-a-user):
```
GET https://api.github.com/users/{username}/events/public
```

## ⚠️ Limitations

- Shows only **public activities** (private repositories not included)
- Limited to **last 10 activities**
- Subject to [GitHub API rate limits](https://docs.github.com/en/rest/overview/resources-in-the-rest-api#rate-limiting)

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📋 Future Enhancements

- [ ] Add activity count parameter (`--count 20`)
- [ ] Filter by activity type (`--type push`)
- [ ] Export to JSON/CSV format
- [ ] Add date range filtering
- [ ] Show commit details for push events
- [ ] Add configuration file support

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Eren Atik** - [@erenatikk](https://github.com/erenatikk)

## 🙏 Acknowledgments

- [GitHub REST API Documentation](https://docs.github.com/en/rest)
- [roadmap.sh](https://roadmap.sh/projects/github-user-activity) - Project inspiration
- .NET Community for excellent documentation

---

⭐ **Star this repo if you found it helpful!**
