import { Options } from 'swagger-jsdoc';

export const swaggerOptions: Options = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'API Node.js Escola',
      version: '1.0.0',
      description: 'Documentação da API Node.js com TypeScript, Prisma e Express',
    },
    components: {
      securitySchemes: {
        bearerAuth: {
          type: 'http',
          scheme: 'bearer',
          bearerFormat: 'JWT',
        },
      },
    },
    security: [
      {
        bearerAuth: [],
      },
    ],
    servers: [
      {
        url: 'http://localhost:3333',
      },
    ],
  },
  apis: ['./src/controllers/*.ts', './src/routes/*.ts'],
};
