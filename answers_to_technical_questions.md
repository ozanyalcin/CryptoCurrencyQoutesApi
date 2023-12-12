### Answers To Technical Questions

- I spent 8-10 hours for this assignment. If I had more time, I would want to add more unit tests which covers more case for query handler,3rd party integration points and middleware. Also I would add retry policy in order to achieve more reliable connection between 3rd party api integration and better validation for controller.


- The most useful feature that was added to the latest version of c# 12 is *Primary Constructor*

```c#
public class Calculation(int a,int b)
{
  public int Addition() => a + b;
  public int Subtraction() => a - b;
}
```

- The production issues became very crucial part in the real world and I also had to track down production issues. First approach might be in terms of metrics. Metrics are predefined values for the system that demonstrate the healthiness of the production. The best way to benefit from metrics is automatizing the alert and health check if proper performance limits are exceeded compared to predefined metrics.
In addition to that one, deciding whether all nodes or a part of the nodes are affected provides chance to move forward. Another approach would be checking the system logs that could give a clue in which part was broken. Investigation the issue regarding network or cpu related also breakdowns the issue into smaller part of the system. Those approaches helps to figure out which part is causing performance issue like in I/O part of DB level, query performance, 3rd party api issue, internal network delay, CPU related json deserializer issues. 


- The latest technical book that I have read is **Designing Data-Intensive Applications**. The book contains very crucial knowledge which helps me deep dive the pros and cons of various technologies for processing and storing data in terms of scalability, consistency, reliability, efficiency, and maintainability.


- Technical assessment was a valuable experience that allowed me to demonstrate my capabilities and challenged me to apply API design, 3rd party api integration, unit testing effectively. I also acquired knowledge regarding crypto currencies.

- I describe myself using json
```json
{
  "name": "Ozan Yalcin",
  "role": "Software Developer",
  "skills": {
    "languages_and_frameworks": ["C#", ".NET Core", "ASP.NET Core API"],
    "dbms": ["MSSQL", "MongoDB", "PostgreSQL", "Redis", "Elasticsearch"],
    "message_brokers_and_streaming": ["RabbitMQ", "Google Pub/Sub", "Azure Service Bus", "Kafka", "NServiceBus"],
    "actor_model": ["Akka.Net", "Microsoft Orleans"],
    "serverless": ["Azure Functions", "AWS Lambda"],
    "source_control": ["Git", "TFS"],
    "ci_cd": ["GoCD", "Teamcity", "Gitlab CI/CD"],
    "methodologies_and_practices": ["Scrum", "Kanban", "SOLID", "TDD", "DDD", "CQRS", "Event Sourcing"]
  },
  "interests": ["Running", "Basketball", "Outdoor activities", "Playing keyboard"],
  "contact": {
    "email": "ozanyalcin13@gmail.com",
    "github": "github.com/ozanyalcin"
  }
}
```