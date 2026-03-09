import { UserRepository } from '../repositories/UserRepository';
import { User } from '@prisma/client';

import { hash } from 'bcryptjs';

export class UserService {
  constructor(private userRepository: UserRepository) {}

  async execute(name: string, email: string, password: string) {
    const userAlreadyExists = await this.userRepository.findByEmail(email);

    if (userAlreadyExists) {
      throw new Error('User already exists.');
    }

    const passwordHash = await hash(password, 8);

    const user = await this.userRepository.create({
      name,
      email,
      password: passwordHash,
    });

    return user;
  }

  async getAll(): Promise<User[]> {
    return this.userRepository.findAll();
  }

  async getProfile(userId: string) {
    const user = await this.userRepository.findById(Number(userId));

    if (!user) {
      throw new Error('User not found');
    }

    const { password, ...userWithoutPassword } = user;
    return userWithoutPassword;
  }
}
