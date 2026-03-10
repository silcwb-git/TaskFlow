namespace TaskFlow.Tests.Featues.Tasks
{
    [Fact]
    public async Task GetTaskByPriority_ShouldReturnOnlyHighPriorityTasks()
    {
        // Arrange
        var priority = TaskPriority.High;
        var mockTasks = new List<Task>
        {
            new Task{id = Guid.NewGuid(), Title = "High Task", priority = TaskPriority.High},
            new Task{id = Guid.NewGuid(), Title = "Low Task", priority = TaskPriority.Low}
        };

        // Act
        var result = await _taskService.GetTaskByPriorityAsync(priority);

        // Then
        Assert.All(result, task => Assert.Equal(priority, task.priority));
    }
}