using System.Threading.Tasks;
using api.GraphQL;
using Api.Database;
using api.GraphQL.Mutations;
using api.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public GraphQLController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            
            var inputs = query.Variables.ToInputs();

            // This is the schema
            var schema = new Schema
            {
                Query = new CarBrandQuery(_db),
                Mutation = new CarBrandModelMutation(_db)
            };

            // This function will either execute query or mutation based on request.
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}