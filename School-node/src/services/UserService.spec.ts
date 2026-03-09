import { describe, it, expect, vi } from 'vitest';
import { UserService } from './UserService';
import { UserRepository } from '../repositories/UserRepository';

describe('UserService', () => {
  it('should be able to create a new user', async () => {
    const userRepositoryMock = {
      create: vi.fn(),
      findByEmail: vi.fn(),
      findAll: vi.fn(),
    } as unknown as UserRepository;

    (userRepositoryMock.findByEmail as any).mockResolvedValue(null);
    (userRepositoryMock.create as any).mockResolvedValue({
      id: 1,
      name: 'John Doe',
      email: 'john@example.com',
      password: 'hashed_password',
      createdAt: new Date(),
      updatedAt: new Date(),
    });

    const userService = new UserService(userRepositoryMock);

    const user = await userService.execute('John Doe', 'john@example.com', '123456');

    expect(user).toHaveProperty('id');
    expect(user.email).toBe('john@example.com');
  });

  it('should not be able to create a user with duplicate email', async () => {
    const userRepositoryMock = {
      create: vi.fn(),
      findByEmail: vi.fn(),
      findAll: vi.fn(),
    } as unknown as UserRepository;

    (userRepositoryMock.findByEmail as any).mockResolvedValue({
      id: 1,
      email: 'john@example.com',
    });

    const userService = new UserService(userRepositoryMock);

    await expect(
      userService.execute('John Doe', 'john@example.com', '123456')
    ).rejects.toThrow('User already exists');
  });

  describe('getProfile', () => {
    it('should be able to get user profile excluding password', async () => {
      const userRepositoryMock = {
        findById: vi.fn(),
      } as unknown as UserRepository;

      (userRepositoryMock.findById as any).mockResolvedValue({
        id: 1,
        name: 'John Doe',
        email: 'john@example.com',
        password: 'secret_password_should_not_return',
      });

      const userService = new UserService(userRepositoryMock);

      const user = await userService.getProfile('1');

      expect(user).toHaveProperty('id');
      expect(user).not.toHaveProperty('password');
      expect(user.name).toBe('John Doe');
    });

    it('should throw error if user does not exist', async () => {
      const userRepositoryMock = {
        findById: vi.fn(),
      } as unknown as UserRepository;

      (userRepositoryMock.findById as any).mockResolvedValue(null);

      const userService = new UserService(userRepositoryMock);

      await expect(userService.getProfile('999')).rejects.toThrow('User not found');
    });
  });
});
