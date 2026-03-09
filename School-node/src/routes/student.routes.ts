import { Router } from 'express';
import { StudentRepository } from '../repositories/StudentRepository';
import { StudentService } from '../services/StudentService';
import { StudentController } from '../controllers/StudentController';
import { ensureAuthenticated } from '../middlewares/auth';

const studentRoutes = Router();

const studentRepository = new StudentRepository();
const studentService = new StudentService(studentRepository);
const studentController = new StudentController(studentService);

studentRoutes.use(ensureAuthenticated);

studentRoutes.post('/', (req, res) => studentController.create(req, res));
studentRoutes.get('/', (req, res) => studentController.index(req, res));
studentRoutes.get('/:id', (req, res) => studentController.show(req, res));
studentRoutes.put('/:id', (req, res) => studentController.update(req, res));
studentRoutes.delete('/:id', (req, res) => studentController.delete(req, res));

export { studentRoutes };
