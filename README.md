# 🐾 PetWorld - AI-Powered Pet Shop Assistant

> **Note**: This application fulfills all requirements specified in the recruitment task, implementing a multi-agent AI system with clean architecture principles, Docker containerization, and a modern web interface.

## 📖 Overview

PetWorld is an intelligent pet shop assistant application that helps customers find the perfect products for their pets using advanced AI technology. The application employs a multi-agent system with iterative refinement to provide accurate, context-aware product recommendations.

### Key Features

- 🤖 **Multi-Agent AI System**: Writer and Critic agents work together to generate and refine responses
- 💬 **Interactive Chat Interface**: Modern, responsive UI for seamless user interaction
- 📊 **Conversation History**: Track and review past interactions
- 🐳 **Docker Support**: Full containerization with Docker Compose
- 🏗️ **Clean Architecture**: Separation of concerns across Domain, Application, Infrastructure, and Presentation layers
- 🔄 **Auto-scroll**: Automatic scrolling to responses for better UX
- 🎨 **Modern UI**: Glass-morphism design with smooth animations

## 🚀 Getting Started

### Prerequisites

- [Docker](https://www.docker.com/get-started) and Docker Compose
- OR [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) for local development

### Quick Start with Docker (Recommended)

> ⚠️ **IMPORTANT**: Before running the application, you **MUST** create a `.env` file with your Azure OpenAI credentials. The application will not start without it!

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd PetWorld
   ```

2. **Configure environment variables**

   Copy `.env.example` to `.env` and fill in your credentials:
   ```bash
   cp .env.example .env
   ```

   Edit `.env` with your Azure OpenAI credentials:
   ```env
   AGENT_API_KEY=your_azure_openai_api_key_here
   AGENT_ENDPOINT=https://your-resource-name.cognitiveservices.azure.com
   ```

3. **Start the application**
   ```bash
   docker compose up
   ```

   > **Note**: The `--build` flag is optional. Use it only when you've made changes to the Dockerfile or application code.

4. **Access the application**

   Once you see the startup message:
   ```
   🐾 PetWorld Application Started Successfully!
   📍 Application is available at: http://localhost:5000
   ```

   Open your browser and navigate to: **http://localhost:5000**

### Local Development Setup

1. **Install dependencies**
   ```bash
   dotnet restore
   ```

2. **Configure User Secrets**
   ```bash
   cd PetWorld.Web
   dotnet user-secrets set "AgentSettings:ApiKey" "your-api-key"
   dotnet user-secrets set "AgentSettings:Endpoint" "your-endpoint"
   ```

   Or edit `appsettings.Development.json` (not committed to Git):
   ```json
   {
     "AgentSettings": {
       "ApiProvider": "AzureOpenAI",
       "ApiKey": "your-api-key",
       "Endpoint": "your-endpoint",
       "DeploymentName": "gpt-4o-mini",
       "MaxIterations": 3
     }
   }
   ```

3. **Start MySQL**
   ```bash
   docker compose up mysql -d
   ```

4. **Run the application**
   ```bash
   dotnet run --project PetWorld.Web
   ```

## 🛠️ Technology Stack

### Backend
- **.NET 9.0** - Latest .NET framework
- **Blazor Server** - Interactive web UI framework
- **C# 13** - Modern C# features
- **Entity Framework Core 9.0** - ORM for database access
- **MySQL 8.0** - Relational database

### AI/ML
- **Azure OpenAI** - GPT-4o-mini for natural language processing
- **OpenAI SDK** - Official OpenAI/Azure OpenAI client library
- **Multi-Agent Architecture** - Writer and Critic agents for iterative refinement

### Frontend
- **Blazor Interactive Server** - Real-time UI updates
- **Bootstrap 5** - Responsive CSS framework
- **Bootstrap Icons** - Icon library
- **Custom CSS** - Glass-morphism design with animations

### Infrastructure
- **Docker** - Containerization
- **Docker Compose** - Multi-container orchestration
- **MySQL** - Database persistence

## 📂 Project Structure

```
PetWorld/
├── PetWorld.Domain/           # Domain entities and business logic
│   └── Entities/              # Product, ChatHistory entities
├── PetWorld.Application/      # Application services and interfaces
│   ├── Services/              # Business services (ChatService, Agents)
│   ├── DTOs/                  # Data Transfer Objects
│   └── Configuration/         # Application configuration
├── PetWorld.Infrastructure/   # Data access and external services
│   ├── Data/                  # DbContext and migrations
│   └── Repositories/          # Repository implementations
├── PetWorld.Web/              # Presentation layer (Blazor)
│   ├── Pages/                 # Razor components
│   ├── Shared/                # Shared components (Layout, NavMenu)
│   └── wwwroot/               # Static files (CSS, JS)
├── docker-compose.yml         # Docker composition
├── Dockerfile                 # Multi-stage Docker build
└── .env.example               # Environment variables template
```

## 🏗️ Architecture

The application follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────┐
│         PetWorld.Web (UI)               │
│         - Blazor Pages                  │
│         - JavaScript Interop            │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│    PetWorld.Application (Services)      │
│    - ChatService (Orchestration)        │
│    - WriterAgent (Response Generation)  │
│    - CriticAgent (Quality Assurance)    │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│  PetWorld.Infrastructure (Data Access)  │
│  - ProductRepository                    │
│  - DbContext                            │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│      PetWorld.Domain (Entities)         │
│      - Product                          │
│      - ChatHistory                      │
└─────────────────────────────────────────┘
```

### Multi-Agent Workflow

```
User Question
     │
     ▼
[ChatService Orchestrator]
     │
     ├──▶ [WriterAgent] ──▶ Generate Response
     │         │
     │         ▼
     └──▶ [CriticAgent] ──▶ Review & Feedback
               │
               ├─ Approved? ─▶ Return Response
               │
               └─ Not Approved? ─▶ Loop back to WriterAgent
                  (Max 3 iterations)
```

## 🔧 Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `MYSQL_ROOT_PASSWORD` | MySQL root password | `root123` |
| `MYSQL_DATABASE` | Database name | `petworld` |
| `AGENT_API_PROVIDER` | AI provider (OpenAI/AzureOpenAI) | `AzureOpenAI` |
| `AGENT_API_KEY` | Azure OpenAI API key | *Required* |
| `AGENT_ENDPOINT` | Azure OpenAI endpoint URL | *Required* |
| `AGENT_DEPLOYMENT_NAME` | Model deployment name | `gpt-4o-mini` |
| `AGENT_MAX_ITERATIONS` | Max refinement iterations | `3` |

### Database

The application uses **MySQL 8.0** with automatic migrations on startup (Development mode). The database includes:

- **Products** table: Pre-seeded with pet shop products
- **ChatHistory** table: Stores conversation history

## 🧪 Testing

To test the application:

1. Navigate to http://localhost:5000
2. Enter a question like: "I'm looking for food for my golden retriever"
3. Watch as the AI agents generate and refine the response
4. Check the iteration count badge to see how many refinements were made
5. View conversation history at `/historia`

## 🎯 Roadmap & Future Improvements

### High Priority

- [ ] **Add Input Validation** - Implement FluentValidation for user inputs
- [ ] **Response Parsing Improvements** - Use JSON instead of regex for critic feedback
- [ ] **Add Unit Tests** - Comprehensive test coverage for services and agents
- [ ] **Integration Tests** - End-to-end testing for multi-agent workflow

### Medium Priority

- [ ] **Add Pagination** - Implement paging for conversation history
- [ ] **DTOs for All Layers** - Complete DTO implementation for layer separation
- [ ] **Performance Monitoring** - Add Application Insights or similar
- [ ] **Caching Layer** - Cache product data and frequent queries

### Low Priority

- [ ] **User Authentication** - Add user accounts and personalized history
- [ ] **Export History** - Allow users to export conversation history (CSV, JSON)
- [ ] **Advanced Filters** - Filter history by date, product category, iteration count
- [ ] **Internationalization** - Multi-language support (English, Polish)
- [ ] **Dark Mode** - Theme toggle for better user experience

### Infrastructure

- [ ] **CI/CD Pipeline** - GitHub Actions or Azure DevOps
- [ ] **Health Checks** - Kubernetes-ready health endpoints
- [ ] **Monitoring & Logging** - Structured logging with Serilog
- [ ] **API Documentation** - Swagger/OpenAPI if REST API is added
- [ ] **Production Secrets Management** - Azure Key Vault or similar

## 🔧 Troubleshooting

### Error: "Failed to convert configuration value at 'AgentSettings:MaxIterations' to type 'System.Int32'"

**Cause**: The `.env` file is missing or not properly configured.

**Solution**:
1. Ensure `.env` file exists in the root directory
2. Copy from template: `cp .env.example .env`
3. Fill in all required values in `.env`:
   - `AGENT_API_KEY`
   - `AGENT_ENDPOINT`
   - Other configuration values

### Container "petworld-web" exits immediately

**Cause**: Missing or invalid environment variables.

**Solution**: Check that your `.env` file contains valid values for all required variables. See `.env.example` for the complete list.

## 🐛 Known Issues

1. **Antiforgery Token Warnings** in Docker - Normal behavior due to container restarts
2. **DataProtection Keys Warning** - Expected in development; needs persistent volume in production

## 📝 License

This project was created as part of a recruitment task.

## 🤝 Contributing

This is a recruitment task project. For questions or suggestions, please contact the repository owner.

---

**Built with ❤️ using .NET 9, Blazor, and Azure OpenAI**

Co-Authored-By: Claude Sonnet 4.5 <noreply@anthropic.com>
