import { Router } from 'express';
import { AcademyRepository } from '../repositories/AcademyRepository';
import { AcademyService } from '../services/AcademyService';
import { AcademyController } from '../controllers/AcademyController';
import { ensureAuthenticated } from '../middlewares/auth';

const academyRoutes = Router();

const academyRepository = new AcademyRepository();
const academyService = new AcademyService(academyRepository);
const academyController = new AcademyController(academyService);

academyRoutes.use(ensureAuthenticated);

academyRoutes.post('/', (req, res) => academyController.create(req, res));
academyRoutes.get('/', (req, res) => academyController.index(req, res));
academyRoutes.get('/:id', (req, res) => academyController.show(req, res));
academyRoutes.put('/:id', (req, res) => academyController.update(req, res));
academyRoutes.delete('/:id', (req, res) => academyController.delete(req, res));

export { academyRoutes };
