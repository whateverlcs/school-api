import { Router } from 'express';
import { UserRepository } from '../repositories/UserRepository';
import { UserService } from '../services/UserService';
import { UserController } from '../controllers/UserController';
import { ensureAuthenticated } from '../middlewares/auth';

const userRoutes = Router();

const userRepository = new UserRepository();
const userService = new UserService(userRepository);
const userController = new UserController(userService);

userRoutes.get('/profile', ensureAuthenticated, (req, res) => userController.profile(req, res));
userRoutes.post('/', (req, res) => userController.create(req, res));
userRoutes.get('/', ensureAuthenticated, (req, res) => userController.index(req, res));

export { userRoutes };
