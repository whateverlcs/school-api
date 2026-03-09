import { Request, Response } from 'express';
import { AuthService } from '../services/AuthService';
import { z } from 'zod';

export class AuthController {
  constructor(private authService: AuthService) {}

  /**
   * @openapi
   * /auth/login:
   *   post:
   *     summary: Autentica um usuário e retorna um token JWT
   *     tags: [Auth]
   *     requestBody:
   *       required: true
   *       content:
   *         application/json:
   *           schema:
   *             type: object
   *             properties:
   *               email: { type: string }
   *               password: { type: string }
   *     responses:
   *       200:
   *         description: Login bem sucedido
   *       400:
   *         description: E-mail ou senha incorretos
   */
  async handle(req: Request, res: Response) {
    const authSchema = z.object({
      email: z.string().email(),
      password: z.string(),
    });

    const { email, password } = authSchema.parse(req.body);

    const result = await this.authService.execute(email, password);

    return res.json(result);
  }
}
