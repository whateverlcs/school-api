import { z } from 'zod';

export const createAcademySchema = z.object({
  name: z.string().min(3, 'Name must be at least 3 characters'),
  address: z.string().min(5, 'Address must be at least 5 characters'),
  state: z.string().length(2, 'State must be a 2-letter abbreviation'),
  city: z.string().min(2, 'City must be at least 2 characters'),
});

export const updateAcademySchema = createAcademySchema.partial();

export type CreateAcademyDTO = z.infer<typeof createAcademySchema>;
export type UpdateAcademyDTO = z.infer<typeof updateAcademySchema>;
