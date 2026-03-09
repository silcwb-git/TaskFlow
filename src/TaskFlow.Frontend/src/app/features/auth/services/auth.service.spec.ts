import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService, LoginResponse } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://localhost:5001/api/auth';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should register a user', (done) => {
    const mockResponse: LoginResponse = {
      userId: '123',
      email: 'test@example.com',
      fullName: 'Test User',
      accessToken: 'token123',
      refreshToken: 'refresh123'
    };

    service.register('test@example.com', 'password123').subscribe(response => {
      expect(response).toEqual(mockResponse);
      expect(localStorage.getItem('accessToken')).toBe('token123');
      done();
    });

    const req = httpMock.expectOne(`${apiUrl}/register`);
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });

  it('should login a user', (done) => {
    const mockResponse: LoginResponse = {
      userId: '123',
      email: 'test@example.com',
      fullName: 'Test User',
      accessToken: 'token123',
      refreshToken: 'refresh123'
    };

    service.login('test@example.com', 'password123').subscribe(response => {
      expect(response).toEqual(mockResponse);
      expect(service.isAuthenticated()).toBe(true);
      done();
    });

    const req = httpMock.expectOne(`${apiUrl}/login`);
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });

  it('should logout a user', () => {
    localStorage.setItem('accessToken', 'token123');
    localStorage.setItem('refreshToken', 'refresh123');

    service.logout();

    expect(localStorage.getItem('accessToken')).toBeNull();
    expect(service.isAuthenticated()).toBe(false);
  });

  it('should return access token', () => {
    localStorage.setItem('accessToken', 'token123');
    expect(service.getAccessToken()).toBe('token123');
  });

  it('should check if user is authenticated', () => {
    expect(service.isAuthenticated()).toBe(false);
    localStorage.setItem('accessToken', 'token123');
    expect(service.isAuthenticated()).toBe(true);
  });
});
