import { Request, Response } from 'express';
import { AcademyService } from '../services/AcademyService';
import { createAcademySchema, updateAcademySchema } from '../dtos/AcademyDTO';

export class AcademyController {
  constructor(private academyService: AcademyService) {}

  /**
   * @openapi
   * /academies:
   *   post:
   *     summary: Cria uma nova academia
   *     tags: [Academies]
   *     security:
   *       - bearerAuth: []
   *     requestBody:
   *       required: true
   *       content:
   *         application/json:
   *           schema:
   *             type: object
   *             properties:
   *               name: { type: string }
   *               address: { type: string }
   *               state: { type: string }
   *               city: { type: string }
   *     responses:
   *       201:
   *         description: Academia criada com sucesso
   *       401:
   *         description: Não autorizado
   */
  async create(req: Request, res: Response) {
    const data = createAcademySchema.parse(req.body);
    const academy = await this.academyService.execute(data);
    return res.status(201).json(academy);
  }

  /**
   * @openapi
   * /academies:
   *   get:
   *     summary: Lista todas as academias ou filtra por nome/estado/cidade
   *     tags: [Academies]
   *     security:
   *       - bearerAuth: []
   *     parameters:
   *       - in: query
   *         name: name
   *         schema:
   *           type: string
   *         description: Nome da academia para filtro
   *       - in: query
   *         name: state
   *         schema:
   *           type: string
   *         description: Estado (UF) para filtro
   *       - in: query
   *         name: city
   *         schema:
   *           type: string
   *         description: Cidade para filtro
   *     responses:
   *       200:
   *         description: Lista de academias
   *       401:
   *         description: Não autorizado
   */
  async index(req: Request, res: Response) {
    const { name, state, city } = req.query;

    if (name) {
      const academies = await this.academyService.getByName(String(name));
      return res.json(academies);
    }

    if (state) {
      const academies = await this.academyService.getByState(String(state));
      return res.json(academies);
    }

    if (city) {
      const academies = await this.academyService.getByCity(String(city));
      return res.json(academies);
    }

    const academies = await this.academyService.getAll();
    return res.json(academies);
  }

  /**
   * @openapi
   * /academies/{id}:
   *   get:
   *     summary: Retorna os detalhes de uma academia
   *     tags: [Academies]
   *     security:
   *       - bearerAuth: []
   *     parameters:
   *       - in: path
   *         name: id
   *         required: true
   *         schema:
   *           type: number
   *     responses:
   *       200:
   *         description: Detalhes da academia
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Academia não encontrada
   */
  async show(req: Request, res: Response) {
    const { id } = req.params;
    const academy = await this.academyService.getById(Number(id));
    return res.json(academy);
  }

  /**
   * @openapi
   * /academies/{id}:
   *   put:
   *     summary: Atualiza os dados de uma academia
   *     tags: [Academies]
   *     security:
   *       - bearerAuth: []
   *     parameters:
   *       - in: path
   *         name: id
   *         required: true
   *         schema:
   *           type: number
   *     requestBody:
   *       content:
   *         application/json:
   *           schema:
   *             type: object
   *             properties:
   *               name: { type: string }
   *               address: { type: string }
   *               state: { type: string }
   *               city: { type: string }
   *     responses:
   *       200:
   *         description: Academia atualizada com sucesso
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Academia não encontrada
   */
  async update(req: Request, res: Response) {
    const { id } = req.params;
    const data = updateAcademySchema.parse(req.body);
    const academy = await this.academyService.update(Number(id), data);
    return res.json(academy);
  }

  /**
   * @openapi
   * /academies/{id}:
   *   delete:
   *     summary: Remove uma academia
   *     tags: [Academies]
   *     security:
   *       - bearerAuth: []
   *     parameters:
   *       - in: path
   *         name: id
   *         required: true
   *         schema:
   *           type: number
   *     responses:
   *       204:
   *         description: Academia removida com sucesso
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Academia não encontrada
   */
  async delete(req: Request, res: Response) {
    const { id } = req.params;
    await this.academyService.delete(Number(id));
    return res.status(204).send();
  }
}
