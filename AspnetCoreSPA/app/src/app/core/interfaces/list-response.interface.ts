import { IPage } from './page.interface';

export interface IListResponse<Model> {
  result: Model[];
  page: IPage;
}
