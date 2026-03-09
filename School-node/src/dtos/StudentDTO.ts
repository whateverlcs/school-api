import { z } from 'zod';

export enum Schooling {
  Elementary_School = 0,
  High_School = 1,
  College = 2
}

export const createStudentSchema = z.object({
  name: z.string().min(3, 'Name must be at least 3 characters'),
  surname: z.string().min(2, 'Surname must be at least 2 characters'),
  age: z.number().min(2, 'Age must be at least 2'),
  email: z.string().email('Invalid email format'),
  schooling: z.nativeEnum(Schooling),
  academyId: z.number(),
});

export const updateStudentSchema = createStudentSchema.partial();

export type CreateStudentDTO = z.infer<typeof createStudentSchema>;
export type UpdateStudentDTO = z.infer<typeof updateStudentSchema>;
