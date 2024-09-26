using CLI.UI;
using FileRepository;  // Reference to FileRepositories project
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");

IUserRepository userRepository= new UserFileRepository(); // Use file-based repository
IPostRepository postRepository = new PostFileRepository();  // Use file-based repository
ICommentRepository commentRepository = new CommentFileRepository();  // Use file-based repository

CliApp cliApp = new CliApp(userRepository, postRepository, commentRepository);
await cliApp.StartAsync();