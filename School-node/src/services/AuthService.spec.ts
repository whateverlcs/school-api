import { describe, it, expect, vi, Mock } from 'vitest';
import { AuthService } from './AuthService';
import { UserRepository } from '../repositories/UserRepository';
import { compare } from 'bcryptjs';
import { sign } from 'jsonwebtoken';

// Mock das bibliotecas externas
vi.mock('bcryptjs', () => ({
  compare: vi.fn(),
  hash: vi.fn(),
}));

vi.mock('jsonwebtoken', () => ({
  sign: vi.fn(),
  verify: vi.fn(),
}));

describe('AuthService', () => {
  it('should be able to authenticate a user', async () => {
    // Mock do Repository
    const userRepositoryMock = {
      findByEmail: vi.fn(),
    } as unknown as UserRepository;

    // Mock do retorno do banco
    (userRepositoryMock.findByEmail as any).mockResolvedValue({
      id: 1,
      name: 'John Doe',
      email: 'john@example.com',
      password: 'hashed_password',
    });

    // Mock do bcrypt (senha bate)
    (compare as Mock).mockResolvedValue(true);
    // Mock do jwt
    (sign as Mock).mockReturnValue('fake_token');

    const authService = new AuthService(userRepositoryMock);

    const result = await authService.execute('john@example.com', '123456');

    expect(result).toHaveProperty('token');
    expect(result.user.email).toBe('john@example.com');
  });

  it('should not be able to authenticate with incorrect email', async () => {
    const userRepositoryMock = {
      findByEmail: vi.fn(),
    } as unknown as UserRepository;

    // Mock do retorno do banco (null = usuário não encontrado)
    (userRepositoryMock.findByEmail as any).mockResolvedValue(null);

    const authService = new AuthService(userRepositoryMock);

    await expect(
      authService.execute('john@example.com', '123456')
    ).rejects.toThrow('Email or password incorrect');
  });

  it('should not be able to authenticate with incorrect password', async () => {
    const userRepositoryMock = {
      findByEmail: vi.fn(),
    } as unknown as UserRepository;

    (userRepositoryMock.findByEmail as any).mockResolvedValue({
      id: 1,
      email: 'john@example.com',
      password: 'hashed_password',
    });

    // Mock do bcrypt (senha NÃO bate)
    (compare as Mock).mockResolvedValue(false);

    const authService = new AuthService(userRepositoryMock);

    await expect(
      authService.execute('john@example.com', 'wrong_password')
    ).rejects.toThrow('Email or password incorrect');
  });
});
