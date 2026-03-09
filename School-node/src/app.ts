import express from 'express';
import cors from 'cors';
import 'express-async-errors';
import helmet from 'helmet';
import rateLimit from 'express-rate-limit';
import pinoHttp from 'pino-http';

import swaggerUi from 'swagger-ui-express';
import swaggerJsDoc from 'swagger-jsdoc';
import { swaggerOptions } from './config/swagger';
import { logger } from './config/logger';

import { userRoutes } from './routes/user.routes';
import { authRoutes } from './routes/auth.routes';
import { academyRoutes } from './routes/academy.routes';
import { studentRoutes } from './routes/student.routes';
import { errorHandler } from './middlewares/errorHandler';

const app = express();
const swaggerDocs = swaggerJsDoc(swaggerOptions);

// Rate limiters
const generalLimiter = rateLimit({
  windowMs: 1 * 60 * 1000, // 1 minuto
  max: 100,
  message: { status: 'error', message: 'Too many requests, please try again later.' },
});

const authLimiter = rateLimit({
  windowMs: 1 * 60 * 1000, // 1 minuto
  max: 10, // Proteção contra brute force
  message: { status: 'error', message: 'Too many login attempts, please try again later.' },
});

app.use(pinoHttp({ logger }));
app.use(express.json());
app.use(cors());
app.use(helmet());
app.use(generalLimiter);

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

app.use('/auth', authLimiter, authRoutes);

app.use('/users', userRoutes);
app.use('/academies', academyRoutes);
app.use('/students', studentRoutes);

app.get('/', (req, res) => {
    res.json({ message: 'API is running!' });
});

app.use(errorHandler);

export { app };
