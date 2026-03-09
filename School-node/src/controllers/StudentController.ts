import { Request, Response } from 'express';
import { StudentService } from '../services/StudentService';
import { createStudentSchema, updateStudentSchema } from '../dtos/StudentDTO';

export class StudentController {
  constructor(private studentService: StudentService) {}

  /**
   * @openapi
   * /students:
   *   post:
   *     summary: Cria um novo aluno
   *     tags: [Students]
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
   *               surname: { type: string }
   *               age: { type: number }
   *               email: { type: string }
   *               schooling: { type: number, enum: [0, 1, 2] }
   *               academyId: { type: number }
   *     responses:
   *       201:
   *         description: Aluno criado com sucesso
   *       401:
   *         description: Não autorizado
   */
  async create(req: Request, res: Response) {
    const data = createStudentSchema.parse(req.body);
    const student = await this.studentService.execute(data);
    return res.status(201).json(student);
  }

  /**
   * @openapi
   * /students:
   *   get:
   *     summary: Lista todos os alunos ou filtra por nome/academia
   *     tags: [Students]
   *     security:
   *       - bearerAuth: []
   *     parameters:
   *       - in: query
   *         name: name
   *         schema:
   *           type: string
   *         description: Nome do aluno para filtro
   *       - in: query
   *         name: academyId
   *         schema:
   *           type: number
   *         description: ID da academia para filtro
   *     responses:
   *       200:
   *         description: Lista de alunos
   *       401:
   *         description: Não autorizado
   */
  async index(req: Request, res: Response) {
    const { name, academyId } = req.query;

    if (name) {
      const students = await this.studentService.getByName(String(name));
      return res.json(students);
    }

    if (academyId) {
      const students = await this.studentService.getByAcademy(Number(academyId));
      return res.json(students);
    }

    const students = await this.studentService.getAll();
    return res.json(students);
  }

  /**
   * @openapi
   * /students/{id}:
   *   get:
   *     summary: Retorna os detalhes de um aluno
   *     tags: [Students]
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
   *         description: Detalhes do aluno
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Aluno não encontrado
   */
  async show(req: Request, res: Response) {
    const { id } = req.params;
    const student = await this.studentService.getById(Number(id));
    return res.json(student);
  }

  /**
   * @openapi
   * /students/{id}:
   *   put:
   *     summary: Atualiza os dados de um aluno
   *     tags: [Students]
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
   *               surname: { type: string }
   *               age: { type: number }
   *               email: { type: string }
   *               schooling: { type: number, enum: [0, 1, 2] }
   *               academyId: { type: number }
   *     responses:
   *       200:
   *         description: Aluno atualizado com sucesso
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Aluno não encontrado
   */
  async update(req: Request, res: Response) {
    const { id } = req.params;
    const data = updateStudentSchema.parse(req.body);
    const student = await this.studentService.update(Number(id), data);
    return res.json(student);
  }

  /**
   * @openapi
   * /students/{id}:
   *   delete:
   *     summary: Remove um aluno
   *     tags: [Students]
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
   *         description: Aluno removido com sucesso
   *       401:
   *         description: Não autorizado
   *       404:
   *         description: Aluno não encontrado
   */
  async delete(req: Request, res: Response) {
    const { id } = req.params;
    await this.studentService.delete(Number(id));
    return res.status(204).send();
  }
}
