import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TaskService, Task, CreateTaskRequest } from './task.service';

describe('TaskService', () => {
  let service: TaskService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://localhost:5001/api/tasks';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TaskService]
    });
    service = TestBed.inject(TaskService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should create a task', (done) => {
    const mockTask: Task = {
      id: '1',
      title: 'Test Task',
      description: 'Test Description',
      status: 'Todo',
      priority: 'High',
      createdAt: new Date().toISOString(),
      dueDate: '2026-03-15T10:00:00'
    };

    const createRequest: CreateTaskRequest = {
      title: 'Test Task',
      description: 'Test Description',
      priority: 'High',
      dueDate: '2026-03-15T10:00:00'
    };

    service.createTask(createRequest).subscribe(task => {
      expect(task).toEqual(mockTask);
      done();
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('POST');
    req.flush(mockTask);
  });

  it('should get all tasks', (done) => {
    const mockTasks: Task[] = [
      {
        id: '1',
        title: 'Task 1',
        status: 'Todo',
        priority: 'High',
        createdAt: new Date().toISOString()
      },
      {
        id: '2',
        title: 'Task 2',
        status: 'Done',
        priority: 'Low',
        createdAt: new Date().toISOString()
      }
    ];

    service.getAllTasks().subscribe(tasks => {
      expect(tasks.length).toBe(2);
      expect(tasks).toEqual(mockTasks);
      done();
    });

    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(mockTasks);
  });

  it('should get task by id', (done) => {
    const mockTask: Task = {
      id: '1',
      title: 'Test Task',
      status: 'Todo',
      priority: 'High',
      createdAt: new Date().toISOString()
    };

    service.getTaskById('1').subscribe(task => {
      expect(task).toEqual(mockTask);
      done();
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockTask);
  });

  it('should update a task', (done) => {
    const updateRequest: CreateTaskRequest = {
      title: 'Updated Task',
      priority: 'Medium'
    };

    service.updateTask('1', updateRequest).subscribe(() => {
      expect(true).toBe(true);
      done();
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(null);
  });

  it('should delete a task', (done) => {
    service.deleteTask('1').subscribe(() => {
      expect(true).toBe(true);
      done();
    });

    const req = httpMock.expectOne(`${apiUrl}/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
