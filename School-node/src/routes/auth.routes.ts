import { Router } from 'express';
import { UserRepository } from '../repositories/UserRepository';
import { AuthService } from '../services/AuthService';
import { AuthController } from '../controllers/AuthController';

const authRoutes = Router();

const userRepository = new UserRepository();
const authService = new AuthService(userRepository);
const authController = new AuthController(authService);

authRoutes.post('/login', (req, res) => authController.handle(req, res));

export { authRoutes };
