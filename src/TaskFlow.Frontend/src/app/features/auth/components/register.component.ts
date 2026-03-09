import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="register-container">
      <div class="register-box">
        <h1>TaskFlow</h1>
        <h2>Registrar</h2>
        
        <form (ngSubmit)="onRegister()">
          <div class="form-group">
            <label for="email">Email:</label>
            <input
              type="email"
              id="email"
              [(ngModel)]="email"
              name="email"
              required
              placeholder="seu@email.com"
            />
          </div>

          <div class="form-group">
            <label for="password">Senha:</label>
            <input
              type="password"
              id="password"
              [(ngModel)]="password"
              name="password"
              required
              placeholder="Sua senha"
            />
          </div>

          <div class="form-group">
            <label for="confirmPassword">Confirmar Senha:</label>
            <input
              type="password"
              id="confirmPassword"
              [(ngModel)]="confirmPassword"
              name="confirmPassword"
              required
              placeholder="Confirme sua senha"
            />
          </div>

          <button type="submit" [disabled]="loading">
            {{ loading ? 'Registrando...' : 'Registrar' }}
          </button>
        </form>

        <p class="login-link">
          Já tem conta? <a (click)="goToLogin()">Faça login aqui</a>
        </p>

        <div *ngIf="error" class="error-message">
          {{ error }}
        </div>
      </div>
    </div>
  `,
  styles: [`
    .register-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    }

    .register-box {
      background: white;
      padding: 40px;
      border-radius: 10px;
      box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
      width: 100%;
      max-width: 400px;
    }

    h1 {
      text-align: center;
      color: #667eea;
      margin-bottom: 10px;
    }

    h2 {
      text-align: center;
      color: #333;
      margin-bottom: 30px;
    }

    .form-group {
      margin-bottom: 20px;
    }

    label {
      display: block;
      margin-bottom: 5px;
      color: #333;
      font-weight: 500;
    }

    input {
      width: 100%;
      padding: 10px;
      border: 1px solid #ddd;
      border-radius: 5px;
      font-size: 14px;
      box-sizing: border-box;
    }

    input:focus {
      outline: none;
      border-color: #667eea;
      box-shadow: 0 0 5px rgba(102, 126, 234, 0.3);
    }

    button {
      width: 100%;
      padding: 12px;
      background: #667eea;
      color: white;
      border: none;
      border-radius: 5px;
      font-size: 16px;
      font-weight: 600;
      cursor: pointer;
      transition: background 0.3s;
    }

    button:hover:not(:disabled) {
      background: #5568d3;
    }

    button:disabled {
      background: #ccc;
      cursor: not-allowed;
    }

    .login-link {
      text-align: center;
      margin-top: 20px;
      color: #666;
    }

    .login-link a {
      color: #667eea;
      cursor: pointer;
      text-decoration: underline;
    }

    .error-message {
      color: #d32f2f;
      background: #ffebee;
      padding: 10px;
      border-radius: 5px;
      margin-top: 20px;
      text-align: center;
    }
  `]
})
export class RegisterComponent {
  email = '';
  password = '';
  confirmPassword = '';
  loading = false;
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onRegister(): void {
    if (!this.email || !this.password || !this.confirmPassword) {
      this.error = 'Por favor, preencha todos os campos';
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.error = 'As senhas não coincidem';
      return;
    }

    this.loading = true;
    this.error = '';

    this.authService.register(this.email, this.password).subscribe({
      next: () => {
        this.router.navigate(['/tasks']);
      },
      error: (err) => {
        this.error = 'Erro ao registrar. Email pode já estar em uso.';
        this.loading = false;
      }
    });
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}
