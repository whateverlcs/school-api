import { Request, Response } from 'express';
import { UserService } from '../services/UserService';
import { z } from 'zod';

export class UserController {
  constructor(private userService: UserService) {}

  /**
   * @openapi
   * /users:
   *   post:
   *     summary: Cria um novo usuário
   *     tags: [Users]
   *     requestBody:
   *       required: true
   *       content:
   *         application/json:
   *           schema:
   *             type: object
   *             properties:
   *               name: { type: string }
   *               email: { type: string }
   *               password: { type: string }
   *     responses:
   *       201:
   *         description: Usuário criado com sucesso
   */
  async create(req: Request, res: Response) {
    const createUserSchema = z.object({
      name: z.string().min(3),
      email: z.string().email(),
      password: z.string().min(6),
    });

    // Validação dos dados de entrada
    const { name, email, password } = createUserSchema.parse(req.body);

    const user = await this.userService.execute(name, email, password);

    return res.status(201).json(user);
  }

  /**
   * @openapi
   * /users:
   *   get:
   *     summary: Lista todos os usuários
   *     tags: [Users]
   *     responses:
   *       200:
   *         description: Lista de usuários
   */
  async index(req: Request, res: Response) {
    const users = await this.userService.getAll();
    return res.json(users);
  }

  /**
   * @openapi
   * /users/profile:
   *   get:
   *     summary: Retorna o perfil do usuário logado
   *     tags: [Users]
   *     security:
   *       - bearerAuth: []
   *     responses:
   *       200:
   *         description: Perfil do usuário
   *       401:
   *         description: Não autorizado
   */
  async profile(req: Request, res: Response) {

    const userId = (req as any).user_id;

    const user = await this.userService.getProfile(userId);

    return res.json(user);
  }
}
