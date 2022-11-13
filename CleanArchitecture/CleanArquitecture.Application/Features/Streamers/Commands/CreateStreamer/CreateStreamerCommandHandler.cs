using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Infrastructure;
using CleanArquitecture.Application.Contracts.Persistence;
using CleanArquitecture.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Features.Streamers.Commands
{



    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {

        private readonly IStreamerRepository _streamerRepository;
        private IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);
           var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exitosamente");

            await SendEmail(newStreamer);
            return newStreamer.Id;

         }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "yleonardperez@outlook.com",
                Body = "La compañia de streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"Errores enviando el email de {streamer.Id}");
            }
        }

    }
}
