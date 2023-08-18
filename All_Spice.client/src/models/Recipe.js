import { Profile } from "./Profile.js"

export class Recipe {
  constructor(data) {
    this.id = data.id
    this.favoriteId = data.favoriteId ? data.favoriteId : null
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}